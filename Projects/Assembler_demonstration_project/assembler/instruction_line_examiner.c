
#include <string.h>
#include <stdio.h>
#include <ctype.h>
#include <stdlib.h>
#include "first_cover.h"
#include "instruction_data_structure.h"
#include "second_cover.h"

#define ERROR_MSG_SIZE 500/*error message's size*/
#define NUM_OF_OPERATIONS 16/*as defined*/
#define OPERAND_DEFINITION 9/*explained below (at 'op_command_rules_array')*/
#define MAX_LABEL_LENGTH 31/*as defined*/

/*the following macros represent the number of bits that each attribute takes in the word*/
#define IRRELEVANT 3
#define GROUP 2
#define OPCODE 4
#define DESTINATION 2
#define SOURCE 2
#define ERA 2

extern int _ic;/*instruction counter*/
extern int _error_mode;
extern char _error_msg[ERROR_MSG_SIZE];/*error message holder*/

/*The following variable's type contains the additional's word's value.
 * It can be either number(immediate or register's number), or label(as string).
 *If the designation number(designation address of the operand(0 to 3), is 2, then the register before the
 * '[' character will be placed in string_val[0], and the other register will be placed in string_val[1] */
typedef union designation_val {
    short num_val;
    char string_val[MAX_LABEL_LENGTH];
} designation_val;

/*The following variable's type holds the designation's number(_des_num), and the designation's value(as described before)*/
typedef struct des_number_and_value_as_struct {
    int _des_num;
    designation_val _val;
} des_number_and_value_as_struct;


int analyze_designation_number(char *_line, int *_line_pos, des_number_and_value_as_struct *des_num_Val_ptr); 
int add_additional_word_to_instruction_table(des_number_and_value_as_struct *des_num_Val_ptr);

/*The following array guards for each instruction command(represented by the location in the array(0 to 15)):
 * how many operation it needs to get(op_command_rules_array[op_code][0].
 * it's operand's source legal designation numbers (op_command_rules_array[op_code][1\2\3\4])
 * it's operand's destination legal designation address numbers (op_command_rules_array[op_code][5\6\7\8])*/
int op_command_rules_array[NUM_OF_OPERATIONS][OPERAND_DEFINITION] = {{2, 1, 1, 1, 1, 0, 1, 1, 1},
                                                                     {2, 1, 1, 1, 1, 1, 1, 1, 1},
                                                                     {2, 1, 1, 1, 1, 0, 1, 1, 1},
                                                                     {2, 1, 1, 1, 1, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {2, 0, 1, 1, 0, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 1, 1, 1, 1},
                                                                     {1, 0, 0, 0, 0, 0, 1, 1, 1},
                                                                     {0, 0, 0, 0, 0, 0, 0, 0, 0},
                                                                     {0, 0, 0, 0, 0, 0, 0, 0, 0}};

/*Functions declarations*/
short converter(short mask, short offset, short subtract_from_offset);

int zero_operands(char *_line, int _line_pos, int _op_code);

int two_operands(char *_line, int _line_pos, int _op_code);

int one_operand(char *_line, int _line_pos, int _op_code);



/*The following function gets the inspected line '_line', its line position '_line_pos', and and operation code '_op_code'.
 * The functions checks for how many operands the command is needed to, and calls the desired function*/
int command_line_examiner(char *_line, int _line_pos, int _op_code) {

    if (op_command_rules_array[_op_code][0] == 1) {/*if the command requires one operand*/

        if (one_operand(_line, _line_pos, _op_code) == -1) {/*call for the caring function*/
            return -1;
        }

    } else if (op_command_rules_array[_op_code][0] == 2) {/*else, if the command requires 2 operands*/

        if (two_operands(_line, _line_pos, _op_code) == -1) {/*call for the caring function*/
            return -1;
        }

    } else if (zero_operands(_line, _line_pos, _op_code) == -1) {/*else, if the commands requires 0 operands, call the desired function */
        return -1;
    }

    return 1;
}


/*The following function gets the inspected line '_line', its line position '_line_pos', and and operation code '_op_code'.
 * The function checks if the line is a valid line, and calls desired functions to add a to the table. returns -1 on error.*/
int zero_operands(char *_line, int _line_pos, int _op_code) {
    short word=0;/*This variable will guard the word to be added to instruction table*/
    int _return_msg;/*return message to check for error*/
    short offset = 15;/*used for converting the word's bits*/
    short mask = 7;/*used for converting the word's bits*/
    char current_char;/*character holder*/
    short _temp;/*temporary variable to help convert the word*/

    while (isspace(current_char = _line[_line_pos])) {
        _line_pos++;
    };

    if (current_char != '\0' && current_char != EOF) {
        strcpy(_error_msg, "The given instruction command cannot have any operands");/*checks if there are any unwanted arguments*/
        return -1;
    }

    _temp = (converter(mask, offset, IRRELEVANT));/*converting word's bits*/
    word = word | _temp;
    word = word | (_op_code << 6);
    _return_msg = add_to_instruction_array(word);

    if (_return_msg == -1) {
        strcpy(_error_msg, "memory allocation failed");
        return -1;
    }

    return 1;
}


/*The following function gets a mask, offset, and subtract_from_offset . it uses these variables in order
 * to return a word to the caller*/
short converter(short mask, short offset, short subtract_from_offset) {
    short result = 0;
    short temp = 0;

    temp = mask | temp;
    temp <<= offset - subtract_from_offset;
    result = result | temp;

    return result;
}

/*The following function is responsible to give the 'converter' function the necessary values to get a word. it gets
 * era\designation_src\designation_dest\opcodegroup\irrelevant values to build the word*/
short convert_instruction_word_to_bits(int era, int designation_src, int designation_dest, int opcode, int group, int irrelevant) {

    short offset = 15;
    short result = 0;
    short mask = irrelevant;

    result = result | converter(mask, offset, IRRELEVANT);
    offset = offset - IRRELEVANT;/*irrelevant resembles the last 111 digits as requested to be placed in the word*/
    mask = group;
    result = result | converter(mask, offset, GROUP);
    offset = offset - GROUP;
    mask = opcode;
    result = result | converter(mask, offset, OPCODE);
    offset = offset - OPCODE;
    mask = designation_src;
    result = result | converter(mask, offset, DESTINATION);
    offset = offset - DESTINATION;
    mask = designation_dest;
    result = result | converter(mask, offset, SOURCE);
    offset = offset - SOURCE;
    mask = era;
    result = result | converter(mask, offset, ERA);

    return result;
}


/*The following function is responsible to give the 'converter' function the necessary values to get an additional word, with designation number 0 or 1.
 * it gets era\val values to determine the word's bits*/
short convert_additional_word_type0_or_type1_to_bits(int are, short val) {
    short offset = 15;
    short result = 0;
    short mask = val;
    int sign = 1;

    if (val < 0) {
        sign = -1;
    }

    result = result | converter(mask, offset, 13);
    offset = offset - 13;
    mask = are;
    result = result | converter(mask, offset, 2);

    if (sign == -1) {/*toggle msb if number is negative*/
        result ^= 1 << 15;
    }

    return result;
}


/*The following function is responsible to give the 'converter' function the necessary values to get an additional word, with designation number 2 or 3.
 * it gets era\r_A\r_B values to determine the word's bits*/
short convert_additional_word_type2_or_type3_to_bits(int era, int r_A, int r_B) {
    short offset = 14;
    short result = 0;
    short mask = r_B;

    result = result | converter(mask, offset, 6);
    offset = offset - 6;
    mask = r_A;
    result = result | converter(mask, offset, 6);
    offset = offset - 6;
    mask = era;
    result = result | converter(mask, offset, 2);
    return result;
}


/*The following function gets the inspected line '_line', its line position '_line_pos', and _op_code,
 * to examine the line, with one operand expectation. It responsible to analyze the operands, calling the converters to convert the word's bits, and adding the words to the desired tables
 * (either to instruction_table or words_to_be_placed(TBP) table.*/
int one_operand(char *_line, int _line_pos, int _op_code) {
    short result;/*result of function calling*/
    char current_char;/*character holder*/

    des_number_and_value_as_struct *des_num_Val_ptr = calloc(1,sizeof(des_number_and_value_as_struct));/*stores the operand's designation data*/

    if (des_num_Val_ptr == NULL) {
        strcpy(_error_msg, "memory allocation failed");
        return -1;
    }

    while (isspace(current_char = _line[_line_pos]) || current_char == '\0' || current_char == EOF) {/*finding the first and only operand*/
        if (current_char == '\0' || current_char == '\n' || current_char == EOF) {/*returns error if no argument is given*/
            strcpy(_error_msg, "Expected 1 operand in given instruction command");
            free(des_num_Val_ptr);
            return -1;
        }

        _line_pos++;
    }

    if (analyze_designation_number(_line, &_line_pos, des_num_Val_ptr) == -1) {/*calling analyze_designation function*/
        free(des_num_Val_ptr);
        return -1;
    }

    while (isspace(current_char = _line[_line_pos])) {/*ignoring whitespace characters*/
        _line_pos++;
    };

    if (current_char != '\0' && current_char != EOF) {/*only one operand is acceptable*/
        strcpy(_error_msg, "only 1 argument is expected in given instruction command");
        free(des_num_Val_ptr);
        return -1;
    }

    if (op_command_rules_array[_op_code][des_num_Val_ptr->_des_num + 5] != 1) {/*checking for designation rules*/
        strcpy(_error_msg, "type of destination operand is illegal for given instruction command");
        free(des_num_Val_ptr);
        return -1;
    }

    result = convert_instruction_word_to_bits(0, 0, des_num_Val_ptr->_des_num, _op_code, 1, 7);/*converts word's bits*/
    if (add_to_instruction_array(result) == -1) {/*adding word to instruction table*/
        strcpy(_error_msg, "Memory allocation failed");
        free(des_num_Val_ptr);
        return -1;
    }
    add_additional_word_to_instruction_table(des_num_Val_ptr);/*adding additional word to TBP table*/
    free(des_num_Val_ptr);
    return 1;
}


/*The following function gets the inspected line '_line', its line position '_line_pos', and _op_code,
 * to examine the line, with two operands expectation. It responsible to analyze the operands, calling the converters to convert the word's bits, and adding the words to the desired tables
 * (either to instruction_table or words_to_be_placed(TBP) table.*/
int two_operands(char *_line, int _line_pos, int _op_code) {
    int result;/*result of function calling*/
    char current_char;/*character holder*/
    des_number_and_value_as_struct *des_num_Val_ptr_src = calloc(1, sizeof(des_number_and_value_as_struct));/*stores the first operand's designation data*/
    des_number_and_value_as_struct *des_num_Val_ptr_dest = calloc(1, sizeof(des_number_and_value_as_struct));/*stores the second operand's designation data*/

    if (des_num_Val_ptr_src == NULL || des_num_Val_ptr_dest == NULL) {
        strcpy(_error_msg, "Memory allocation failed");
        return -1;
    }

    while (isspace(current_char = _line[_line_pos]) || current_char == '\0' || current_char == EOF) {/*if no operands are given*/
        if (current_char == '\0' || current_char == '\n' || current_char == EOF) {
            strcpy(_error_msg, "missing operands in given instruction command");
            free(des_num_Val_ptr_src);
            free(des_num_Val_ptr_dest);
            return -1;
        }
        _line_pos++;
    }

    if (analyze_designation_number(_line, &_line_pos, des_num_Val_ptr_src) == -1) {/*analyze first operand*/
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    while (isspace(current_char = _line[_line_pos])) {/*ignoring whitespace characters*/
        _line_pos++;
    }
    if (current_char != ',') {/*expecting to get a comma character*/
        strcpy(_error_msg, "missing comma in given instruction command");
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }
    _line_pos++;

    while (isspace(current_char = _line[_line_pos])) {
        _line_pos++;
    }
    if (current_char == '\0' || current_char == EOF) {/*if 2nd operand is missing*/
        strcpy(_error_msg, "2nd operand is expected after comma in given instruction command");
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    if (analyze_designation_number(_line, &_line_pos, des_num_Val_ptr_dest) == -1) {/*analyze 2nd operand*/
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    while (isspace(current_char = _line[_line_pos])) {/*ignoring whitespace characters*/
        _line_pos++;
    }
    if (current_char != '\0' && current_char != EOF) {/*only whitespace can appear after the 2nd operand*/
        strcpy(_error_msg, "only white space characters can be put after the second operand");
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    if (op_command_rules_array[_op_code][des_num_Val_ptr_src->_des_num + 1] != 1) {/*checking for command's first operand designation rules*/
        strcpy(_error_msg, "type of source operand is illegal for given instruction command");
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    if (op_command_rules_array[_op_code][des_num_Val_ptr_dest->_des_num + 5] != 1) {/*checking for command's 2nd operand designation rules*/
        strcpy(_error_msg, "type of destination operand is illegal for given instruction command");
        free(des_num_Val_ptr_dest);
        free(des_num_Val_ptr_src);
        return -1;
    }

    result = convert_instruction_word_to_bits(0, des_num_Val_ptr_src->_des_num, des_num_Val_ptr_dest->_des_num, _op_code, 2, 7);/*converting the word bits(the one before the additional words)*/

    if (add_to_instruction_array(result) == -1) {/*adding the word to the instruction table*/
        strcpy(_error_msg, "Memory allocation failed");
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    /*taking care if both operands are of designation type 3*/
    if (des_num_Val_ptr_src->_des_num == 3 &&
        des_num_Val_ptr_dest->_des_num == 3) {/* both operands are registers, meaning designation 3*/
        des_num_Val_ptr_dest->_val.string_val[0] = des_num_Val_ptr_src->_val.string_val[1];
        if (add_additional_word_to_instruction_table(des_num_Val_ptr_dest) == -1) {
            free(des_num_Val_ptr_src);
            free(des_num_Val_ptr_dest);
            return -1;
        }
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return 1;
    }
    /*if source operand's designation is of type 3*/
    if (des_num_Val_ptr_src->_des_num == 3) {
        des_num_Val_ptr_src->_val.string_val[0] = des_num_Val_ptr_src->_val.string_val[1];
        des_num_Val_ptr_src->_val.string_val[1] = '0';
        if (add_additional_word_to_instruction_table(des_num_Val_ptr_src) == -1) {
            free(des_num_Val_ptr_src);
            free(des_num_Val_ptr_dest);
            return -1;
        }

    } else

    if (add_additional_word_to_instruction_table(des_num_Val_ptr_src) == -1) {/*adding 1st additional word*/
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    if (add_additional_word_to_instruction_table(des_num_Val_ptr_dest) == -1) {/*adding 2nd additional word*/
        free(des_num_Val_ptr_src);
        free(des_num_Val_ptr_dest);
        return -1;
    }

    free(des_num_Val_ptr_src);
    free(des_num_Val_ptr_dest);
    return 1;
}



/*The following function gets des_num_Val_ptr variable, and adds an additional word to TBP table("to be placed"),
 * according to the desired des_num_Val_ptr's values. it uses the converters functions to position the word's bits*/
int add_additional_word_to_instruction_table(des_number_and_value_as_struct *des_num_Val_ptr) {
    int result;/*result of function calling*/

    switch (des_num_Val_ptr->_des_num) {/*checking for right designation number*/

        case 0:

            result = convert_additional_word_type0_or_type1_to_bits(0, des_num_Val_ptr->_val.num_val);
            if(add_to_instruction_array(result)==-1){
		return -1;
	    }
            break;

        case 1:

            if (add_to_tmp_lbl_TBP_list(des_num_Val_ptr->_val.string_val) == -1) {
                return -1;
            }
            break;

        case 2:/*string_val[0] and string_val[1] contain the register's numbers*/

            result = convert_additional_word_type2_or_type3_to_bits(0, des_num_Val_ptr->_val.string_val[0] - '0', des_num_Val_ptr->_val.string_val[1] - '0');
            if(add_to_instruction_array(result)==-1){
               return -1;
	    }
            break;

        case 3:

            result = convert_additional_word_type2_or_type3_to_bits(0, des_num_Val_ptr->_val.string_val[1] - '0', des_num_Val_ptr->_val.string_val[0] - '0');
            if(add_to_instruction_array(result)==-1){
		return -1;
            }
            break;

    }

  return 1;
}


/*The following function gets a _line ,current position _line_pos, and des_num_Val_ptr value.
 * it stores in des_num_Val_ptr value the operand's data. returns -1 on error*/
int analyze_designation_number(char *_line, int *_line_pos, des_number_and_value_as_struct *des_num_Val_ptr) {
    int _current_char;/*character holder*/
    int temp_line_pos = *_line_pos;
    short number;/*number holder*/
    char _possible_lable[MAX_LABEL_LENGTH] = {0};/*label holder*/
    int _label_counter = 0;/*char array counter*/

    memset(des_num_Val_ptr->_val.string_val, '\0', sizeof(des_num_Val_ptr->_val.string_val));

    if ((_current_char = _line[*_line_pos]) == '#') {/*if possible designation 0*/
        temp_line_pos += 1;
        if (analyze_number(_line, &temp_line_pos, &number) == -1) {/*checks for valid number*/
            return -1;
        }

        if (number > 4095 || number < -4096)  /* if out of range(of 6 bits) */
        {
            strcpy(_error_msg, "immediate number exceeds range.(-4096<=range<=4095)");
            return -1;
        }

        *_line_pos = temp_line_pos;/*setting des_num_Val_ptr value*/
        des_num_Val_ptr->_des_num = 0;
        des_num_Val_ptr->_val.num_val = number;
        return 1;
    }/*end '#'*/

    else if ((_current_char = _line[*_line_pos]) == 'r') {/*if possible designation 3*/
        (*_line_pos)++;
        if (!isdigit(_current_char = _line[*_line_pos]) || ((_current_char - '0') > 7)) {/*if its not a register, but maybe a label*/
            char inner_possible_lable[MAX_LABEL_LENGTH] = {0};
            int inner_label_counter = 2;
            inner_possible_lable[0] = 'r';/*guards characters*/
            inner_possible_lable[1] = _current_char;
            (*_line_pos)++;
            _current_char = _line[*_line_pos];
            while (!(isspace(_current_char)) && _current_char != '\0' && _current_char != EOF) {
                inner_possible_lable[inner_label_counter++] = _current_char;
                (*_line_pos)++;
                _current_char = _line[*_line_pos];
                if (_current_char == ',') {
                    break;
                }

            }
            inner_possible_lable[strlen(inner_possible_lable)] = ':';
            if (is_label(inner_possible_lable) == -1) {/*checks if it is a label*/
                strcpy(_error_msg, "incorrect label syntax in the given instruction command");
                return -1;
            }
            inner_possible_lable[strlen(inner_possible_lable) - 1] = '\0';
            des_num_Val_ptr->_des_num = 1;/*setting des_num_Val_ptr*/
            strcpy(des_num_Val_ptr->_val.string_val, inner_possible_lable);
            return 1;
        }
        (*_line_pos)++;
        if ((_current_char = _line[*_line_pos]) == '[') {/*if possible designation 2*/
            if ((((_line[(*_line_pos) - 1] - '0') % 2) == 0) || ((_line[(*_line_pos) - 1] - '0') > 7)) {/*checks for legal writing.*/
                strcpy(_error_msg, "first register before '[' must be an odd number in the given instruction command");
                return -1;
            }
            des_num_Val_ptr->_val.string_val[0] = _line[(*_line_pos) - 1];
            (*_line_pos)++;
            if ((_current_char = _line[*_line_pos]) != 'r') {

                strcpy(_error_msg, "expected 'r' after '['  in the given instruction command");
                return -1;
            }
            (*_line_pos)++;
            if (!isdigit(_current_char = _line[*_line_pos]) || (((_current_char - '0') % 2) != 0) ||
                (_current_char - '0') > 6) {/*checks for legal writing.*/
                strcpy(_error_msg, "the register after '[' must be an even number not greater than 6");
                return -1;
            }

            des_num_Val_ptr->_val.string_val[1] = _current_char;
            (*_line_pos)++;

            if ((_current_char = _line[*_line_pos]) != ']') {
                strcpy(_error_msg, "missing ending ']' after the second register ");
                return -1;
            }

            des_num_Val_ptr->_des_num = 2;/*setting des_num_Val_ptr*/
            (*_line_pos)++;

            return 1;
        }/*end if possible designation 2*/
        _current_char = _line[(*_line_pos) - 1];/*in this section, designation 0 is found*/
        des_num_Val_ptr->_val.string_val[0] = ('0');/*setting des_num_Val_ptr*/
        des_num_Val_ptr->_val.string_val[1] = (_current_char);
        des_num_Val_ptr->_des_num = 3;
        return 1;
    }/*end 'r'*/

    else { /* possible designation 1*/
        while (!(isspace(_current_char)) && _current_char != '\0' && _current_char != EOF) {/*trimming for checking if label*/
            _possible_lable[_label_counter++] = _current_char;
            (*_line_pos)++;
            _current_char = _line[*_line_pos];
            if (_current_char == ',') {
                break;
            }
        }
        _possible_lable[strlen(_possible_lable)] = ':';
        if (is_label(_possible_lable) == -1) {/*if label is incorrect syntax*/
            strcpy(_error_msg, "incorrect label syntax in the given instruction command");
            return -1;
        }

        _possible_lable[strlen(_possible_lable) - 1] = '\0';
        des_num_Val_ptr->_des_num = 1;/*setting des_num_Val_ptr value*/
        strcpy(des_num_Val_ptr->_val.string_val, _possible_lable);
        
    }
 return 1;
}
