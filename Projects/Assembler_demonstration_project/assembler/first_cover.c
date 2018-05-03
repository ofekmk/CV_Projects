
#include "first_cover.h"
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include "directive_line_examiner.h"
#include "instruction_line_examiner.h"
#include "label_data_structure.h"
#include "directive_cmd_data_structure.h"

#define ERROR_MSG_SIZE 500/* size of error message holder*/
#define MAX_NUMBERS_OF_DATA_LINE 100 /* max assumpted given numbers in .data line(deuced from the universal assumption that each line does not exceeds 80 characters)*/
#define NUM_OF_OPERANDS 16 /* as defined*/
#define MAX_LABEL_LENGTH 31/*as defined*/

extern int _dc;/*data counter*/
extern int _ic;/*instruction counter*/
extern char _error_msg[ERROR_MSG_SIZE];/*error message holder*/
extern int _error_mode;


/* '_opcode_as_String' array guards the names of the instruction commands. it helps to find if the given argument is one of the commands in the array*/
char *_opcode_as_String[NUM_OF_OPERANDS] = {"mov", "cmp", "add", "sub", "not", "clr", "lea", "inc", "dec", "jmp", "bne",
                                            "red", "prn", "jsr", "rts", "stop"};

/*'_valid_registers' array guards the computer's registers. It helps to validate if a given register is indeed found in the computer's */
char *_valid_registers[8] = {"r0", "r1", "r2", "r3", "r4", "r5", "r6", "r7"};




/* The following function is called from main after getting a line from the file. The function is the "headquarter" of most of the program's functions.
 * It trims the line, and delivers the desired arguments to the responsible functions. It checks if line is empty, or if it holds a comment.
 * If none of the above is found, it calls for is_label function to check if there is a label in the beginning of the line.
 * It checks if it has a directive or instruction commands, and deliver it's arguments to the caring functions. The function also deters if the command is not recognized.
 * The function will return the first encountered error in any of the steps,if error is found. The function will return value 1 if the line is valid, back to the main function.*/
int first_cover(char *_line) {


    typedef enum type_of_label {/*The following enum holds all of the possible types of the labels, as described below*/
        lbl_not_found = -1,/*label not found in symbols table*/
        entryLBL = 0,/* label is type of entry*/
        extLBL = 1,/* label is type of extern*/
        regularLBL = 2, /*label is type of regular declared label*/
        directiveLBL = 5, /*label is type of a regular label, that was declared before a .string/.data line*/
        ent_and_regLBL = 10, /* label was declared both of regular type and entry type*/
        ent_and_directiveLBL = 15 /*label was declared both of regular-directive type and entry type*/
    } type_of_label;/* this value will be used for determing labels types*/
    type_of_label type_of_label_enum;
    int num_array_of_data_line_length;/*length of .data line's numbers array*/
    int num_array_of_data_line[MAX_NUMBERS_OF_DATA_LINE] = {0};/*array that holds .data line numbers*/
    int _is_labled;/*label is declared?*/
    int _line_pos = 0;/*holds line position*/
    int current_char;/*temporary character holder*/
    int _current_arg_pos = 0;/*position in '_current_arg' argument*/
    char _current_arg[MAX_LABEL_LENGTH] = {0};/*_current_arg holds temporary line's argument*/
    int _is_extern;/* is directive command an extern command?*/
    int i;/*loop counter*/
    int _op_code_as_num = -1;/*numeric presentation of operation code*/
    char _temp_label[MAX_LABEL_LENGTH] = {0};/*temporary argument holder*/



    while (isspace(current_char = _line[_line_pos])) {/*ignoring leading whitespace characters*/
        _line_pos++;
    };

    if (current_char == '\0' || current_char == EOF || current_char == ';') {/*return 1 if line is empty or it is a comment line*/
        return 1;
    }

    while (!(isspace(current_char)) && !(current_char == '\0') && !(current_char == EOF)) {
        _current_arg[_current_arg_pos++] = current_char;
        _line_pos++;
        current_char = _line[_line_pos];
    }

    if (strlen(_current_arg) > (MAX_LABEL_LENGTH - 1)) {/*return error if label/argument is greater than 30 characters*/
        strcpy(_error_msg, "label cannot exceed 30 characters");
        return -1;
    }

    _is_labled = is_label(_current_arg);/*calling function to check if we got a label*/
    if (_is_labled != -1) {
        _current_arg[strlen(_current_arg) - 1] = '\0';
        strcpy(_temp_label, _current_arg);
        if ((type_of_label_enum = search_label(_current_arg)) == extLBL || type_of_label_enum == regularLBL ||   /*label cannot be added if it was already declared(except for entry type)*/
            type_of_label_enum == directiveLBL || type_of_label_enum == ent_and_regLBL ||
            type_of_label_enum == ent_and_directiveLBL) {
            strcpy(_error_msg, "Error Detected - Label with the same name was already given : ");
            strcat(_error_msg, _current_arg);
            return -1;

        } else if (type_of_label_enum == lbl_not_found) {/*add declared label to symbol table, if label is not already found in the table*/
                  if (add_label_to_symbols_array(_current_arg, regularLBL) == -1) {
                      strcpy(_error_msg, "Error Detected - memory allocation failed\n");
                return -1;
                  }

        } else {/*update label's type if it was found as entry type*/
            set_label_type(_current_arg, ent_and_regLBL);
            update_entry_label_address(_current_arg);
        }

        memset(_current_arg, '\0', sizeof(_current_arg));/*preparing to get the next argument*/
        _current_arg_pos = 0;
        current_char = _line[_line_pos];

        while (isspace(current_char)) {/*ignoring whitespaces characters*/
            _line_pos++;
            current_char = _line[_line_pos];
        }

        /*bwws*/
        while (!(isspace(current_char)) && !(current_char == '\0') && !(current_char == EOF)) {/*getting the next argument*/
            _current_arg[_current_arg_pos++] = current_char;
            _line_pos++;
            current_char = _line[_line_pos];
        }
    }

    if (_current_arg[0] == '.') {/*if the argument is a possible directive command*/

        if (strcmp(_current_arg, ".data") == 0) {/* if it is a .data line*/
            if (strlen((_temp_label)) != 0) {
                set_directive_label_typee(_temp_label);/*update the declared label's address to _dc counter*/
            }
            if ((num_array_of_data_line_length = data_line_interpreter(_line, _line_pos, num_array_of_data_line)) == -1) {/*gets a natural number of how many numbers to add, -1 on error*/
                return -1;

            } else if (add_numbers_to_data_array(num_array_of_data_line, num_array_of_data_line_length) == -1) {/* adds the numbers to data table*/
                strcpy(_error_msg, "Error Detected - memory allocation failed");
                return -1;
            }

            return 1;/*go back to main(line is fully examine, and was taken care of)*/
        }/*end if data*/

        else if (strcmp(_current_arg, ".string") == 0) {/*if it is a .string line*/
            if (strlen((_temp_label)) != 0) {
                set_directive_label_typee(_temp_label);/*update the declared label's address to _dc counter*/
            }

            memset(_current_arg, '\0', sizeof(_current_arg));

            if (trim_string(_line, _line_pos, _current_arg) == -1) {/*calling trim_string function*/
                return -1;

            } else if (add_string_to_data_array(_current_arg)==-1) {/*add string characters to the data table*/

                strcpy(_error_msg, "Error Detected - memory allocation failed");
                return -1;
            }
            return 1;/*go back to main(line is fully examine, and was taken care of)*/

        } else if (strcmp(_current_arg, ".entry") == 0 || strcmp(_current_arg, ".extern") == 0) {/*if line is type of entry or extern*/
            if (strlen(_temp_label) != 0) {
                remove_label_node_from_symbol_table(_temp_label);/*ignoring predeclared label(removing from symbol's list) bws*/
            }
            if (strcmp(_current_arg, ".entry") == 0) {
                _is_extern = 0;

            } else {
                _is_extern = 1;
            }

            memset(_current_arg, '\0', sizeof(_current_arg));

            if (get_val_of_entry_or_ext(_line, _current_arg, _line_pos) == -1) {/*checks for the next argument, guards it in _current_arg*/
                return -1;/*if the next arg is not a valid label, return an error*/
            }

            if (_is_extern == 1) {/*if line is of extern type*/
                if ((search_label(_current_arg)) == lbl_not_found || (search_label(_current_arg)) == extLBL) {/*add label to table iff the label was not declared before*/
                    if (add_label_to_symbols_array(_current_arg, _is_extern) == -1) {
                        strcpy(_error_msg, "Error Detected - memory allocation failed\n");
                        return -1;
                    }

                } else {/*returns error if label was declared..*/
                    strcpy(_error_msg, "cannot add the given extern label because it's name already appears: ");
                    strcat(_error_msg, _current_arg);/*ws*/
                    return -1;
                }

            } else {/*else - if line is type of entry*/
                if ((type_of_label_enum = search_label(_current_arg)) == extLBL) {/*returns error if label was declared with extern type */
                    strcpy(_error_msg, "Label with the same name was already given: ");
                    strcat(_error_msg, _current_arg);/*ws*/
                    return -1;
                }
                if (type_of_label_enum == lbl_not_found) {/*add label otherwise*/
                    if (add_label_to_symbols_array(_current_arg, _is_extern) == -1) {
                        strcpy(_error_msg, "memory allocation failed\n");
                        return -1;
                    }

                } else {
                    if (type_of_label_enum == directiveLBL) {/*update lable's type if label was found as regular type or directive type(types explained above)*/
                        set_label_type(_current_arg, ent_and_directiveLBL);

                    } else if (type_of_label_enum == regularLBL) {
                        set_label_type(_current_arg, ent_and_regLBL);

                    } else if (type_of_label_enum == entryLBL) { ;
                    }
                }
            }
            return 1;
        }

        strcpy(_error_msg, "Unidentified directive command");/*if command(that starts with a dot) is unidentified*/
        return -1;
    }

    else {
        for (i = 0; i < 16; i++) {/*checks if command is type of instruction command*/
            if (strcmp(_current_arg, _opcode_as_String[i]) == 0) {
                _op_code_as_num = i;
                break;
            }
        }

        if (_op_code_as_num == -1) {/*command is unidentified*/
            strcpy(_error_msg, "Unidentified command.");
            return -1;
        }

        if (command_line_examiner(_line, _line_pos, _op_code_as_num) == -1) {/*calls the caring function to trim and act according the instruction line. returns -1 if error found*/
            return -1;
        }
    }
   return 1;
}


/* The following function gets an argument '_current_arg' and deters if the argument is a valid label. returns -1 on error*/
int is_label(char *_current_arg) {
    int _current_arg_counter = 0;/*argument's char position*/
    int current_char;/*character holder*/
    int i;

    if (!(isalpha(_current_arg[_current_arg_counter]))) {/*if label is not beginning with an alphabetic character*/
        return -1;
    }

    _current_arg_counter++;
    current_char = _current_arg[_current_arg_counter];

    while (current_char != ':' && current_char != '\0') {/*going through the argument's characters*/
        if (!(isalpha(current_char)) && !(isdigit(current_char))) {
            return -1;
        }
        _current_arg_counter++;
        current_char = _current_arg[_current_arg_counter];
    }

    if (current_char != ':') {
        return -1;

    } 

        
        _current_arg[strlen(_current_arg) - 1] = '\0';

        for (i = 0; i < 16; i++) {/*returns error if label is equal to an assembly's safe word*/
            if (strcmp(_current_arg, _opcode_as_String[i]) == 0) {
                strcpy(_error_msg, "label's name cannot have an assembly's instruction command safe word ");
                return -1;
            }
        }
        for (i = 0; i < 8; i++) {/*returns error if label is equal to an assembly's safe word*/
            if (strcmp(_current_arg, _valid_registers[i]) == 0) {
                strcpy(_error_msg, "label's name cannot have an assembly's register safe word ");
                return -1;
            }
        }
        _current_arg[strlen(_current_arg)] = ':';
        return strlen(_current_arg);/*returns the length of the label*/
    
}


/*he following function gets the inspected line '_line', its line position '_line_pos'. It stores inside 'num' a validated number. returns -1 on error*/
short analyze_number(char *_line, int *_line_pos, short *num) {
    char _number_as_string[100] = {0};/*variable that stores digits*/
    int current_char;/*character holder*/
    int _sign;/*number's sign*/
    int _num_as_string_counter;/* '_number_as_string' array counter*/

    current_char = _line[*_line_pos];
    _num_as_string_counter = 0;
    _sign = 1;

    if (current_char == '+') {/*if + character is found*/
        _sign = 1;
        (*_line_pos)++;
        current_char = _line[*_line_pos];

    } else if (current_char == '-') {/*if - character is found*/
        _sign = -1;
        (*_line_pos)++;
        current_char = _line[*_line_pos];
    }
    if (!(isdigit(current_char))) {/*if non digit character found*/
        strcpy(_error_msg, "incorrect argument (number expected) ");
        return -1;
    }
    while (isdigit(current_char)) {/*add digits to temporary array*/
        _number_as_string[_num_as_string_counter++] = current_char;
        (*_line_pos)++;
        current_char = _line[*_line_pos];
    }

    if (_number_as_string[0] == '\0') {/*if no digits were added to temporary array*/
        strcpy(_error_msg, "invalid argument - number is expected ");
        return -1;
    }
    *num = (atoi(_number_as_string)) * _sign;
    if ((*num) > 16383 || (*num) < -16384) {/*if number exceeds 15 bits*/
        strcpy(_error_msg, "given number is out of range (-16384<=range<=16383) ");
        return -1;
    }
    return 1;
}




