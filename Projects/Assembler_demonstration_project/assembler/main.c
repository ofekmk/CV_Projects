

#define MAX_FILE_NAME 40/*max file name*/
#define MAX_LINE 100/*max line's  length assumption*/
#define ERROR_MSG_SIZE 500/*error message's length*/
#define _IC_INITIALIZED_ADDRESS 100/*initialized instruction counter's value*/
#define _DC_INITIALIZED_VAL 0/*initialized data counter's value*/

#include <stdio.h>
#include <string.h>
#include "first_cover.h"
#include "label_data_structure.h"
#include "second_cover.h"
#include "instruction_data_structure.h"
#include "directive_cmd_data_structure.h"

/* all input\output methods are being done only through this class*/

/*all table's heads*/
extern second_cover_err_msg *second_cover_err_msg_head;
extern instruction_word_list *instruction_word_list_head;
extern data_array *_data_array_head;
extern symbols_array *_symbols_head;
extern temp_label_to_be_placed_list *temp_lblTBP_head;

int _ic = _IC_INITIALIZED_ADDRESS;/*instruction counter*/
int _dc = _DC_INITIALIZED_VAL;/*data counter*/
char _error_msg[ERROR_MSG_SIZE] = {0};/*error message holder*/
int _error_mode = 0;/*error found?*/
int _line_counter = 0;/*current line in file*/
int instruction_table_size, words_TBP_table_size, data_table_size; /*length of tables*/



/*function declarations*/
int print_ob_file(char *_input_file_name_temp);

int print_ent_file(char *_input_file_name_temp);

int print_ext_file(char *_input_file_name_temp);

int call_second_cover(char *_input_file_name_temp);

void reset_all_related_variables(void);



/*main function is responsible to get files, openning them one by one. For each file, it delivers each line(until EOF) to first_cover class.
 * if the result is -1, than error will be printed to screen. after reviewing all of the file's lines, it calls the second cover operator. if error found, errors will be printed accordingly.
 * Else, output files will be created with desired results. */
int main(int argc, char **argv) {
    int _is_error = 0;/*gets a value from first_cover function - checks if an error was encountered in a line*/
    char _input_file_name[MAX_FILE_NAME] = {0};/*input file's name*/
    char _input_file_name_temp[MAX_FILE_NAME] = {0};/*input file's name(temp variable)*/
    char _line[MAX_LINE] = {0};/*line holder*/
    FILE *_curr_file = NULL;/*file pointer to the given file*/
    

    argv++;
    while (*argv) {/*iteration that goes through all input assembly files*/
        strcpy(_input_file_name, *argv);
        strcpy(_input_file_name_temp, _input_file_name);
        strcat(_input_file_name, ".as");
        if ((_curr_file = fopen(_input_file_name, "r")) == NULL) {/*if openning the file has failed*/
            fprintf(stderr, "Error - Cannot open file \"%s\" in \"r\" mode\n", _input_file_name);
            argv++;
            continue;
        }

        while (1) {/* The while loop will iterate through all of the file's lines, until it hits EOF*/
            if (fgets(_line, MAX_LINE, _curr_file) != NULL) {
                _line_counter++;
                _is_error = first_cover(_line);/*calling first_cover function with _line as parameter*/
                if (_is_error == -1) {
                    if (_error_mode == 0) {/*if we encountered a first error*/
                        _error_mode = -1;
                        fprintf(stderr, "%s %s %s","********ERRORS FOUND IN FILE: ",_input_file_name,"*********\n\n");/*header*/
                    }
                    fprintf(stderr,"%s %4d %s %s %c", "IN LINE: ",_line_counter, " MESSAGE: ",_error_msg,'\n');/*printing error*/
                }
                memset(_error_msg, '\0', sizeof(_error_msg));
            } else {
                break;
            }
        }
        /*getting table sizes*/
        data_table_size = compute_data_table_size();
        instruction_table_size = compute_instruction_table_size();
        words_TBP_table_size = compute_TBP_table_size();

        /*second cover*/
        if (call_second_cover(_input_file_name_temp) == -1) {/*calling second cover*/
            if (_error_mode == 0) {/*if first error encountered, header will be printed*/
                _error_mode = -1;
                fprintf(stderr, "%s %s %s","********ERRORS FOUND IN FILE: ",_input_file_name,"*********\n\n");/*header*/
            }
            fprintf(stderr,"%s %c ", " memory allocation failed",'\n');/*printing error*/
        }

/*18;21 27 33 36 39 45 48 51 54 57 60 63 66 69 75 78 81 84 90 93 96 99 102 108 111 114 117 129 138 141 144 150 153 159 162 165 168*/

        if(_error_mode==-1){
            fprintf(stderr,"%c",'\n');/*printing error*/
        }

        reset_all_related_variables();/*resetting all necessary variables*/
        argv++;/*advancing to get next file's name, if exists*/
    }
    fprintf(stdout,"%s %d %s ", " Total number of files received: ",argc-1,"\n");/*printing error*/
    return 0;
}


/* The following function gets an _input_file_name_temp and prints to '_input_file_name_temp.ob' the desired results. returns -1 on error*/
int print_ob_file(char *_input_file_name_temp) {
    char _output_file_name_ob[500] = {'\0'};
    FILE *output_file_ob = NULL;

    instruction_word_list *instruction_word_list_temp_node = instruction_word_list_head;
    data_array *data_array_temp_node = _data_array_head;
    strcpy(_output_file_name_ob, _input_file_name_temp);
    strcat(_output_file_name_ob, ".ob");/*appending suffix*/

    if ((output_file_ob = fopen(_output_file_name_ob, "w")) == NULL) {/*opening file failed*/
        strcpy(_error_msg, "\n memory allocation failed - couldn't create file: ");
        strcat(_error_msg, _input_file_name_temp);
        strcat(_error_msg, ".ob");
        return -1;
    }

    fprintf(output_file_ob,"%s", "Base 16 address:            Base 16 code:\n");/*printing headers*/
    fprintf(output_file_ob,"%s", "\n");
    fprintf(output_file_ob,"%s %X %c %s %X %c ","[instruction words: ",instruction_table_size + words_TBP_table_size,']',"[data words: ",data_table_size,']');
    fprintf(output_file_ob,"%s", "\n\n");

    while (instruction_word_list_temp_node) {/*iterating through instruction table, printing it's words*/
        fprintf(output_file_ob, "  %4X %25s %04X ", instruction_word_list_temp_node->_address," ", instruction_word_list_temp_node->_instruction_word);/* !!! change to hex */
        fprintf(output_file_ob, "%s", "\n");
        instruction_word_list_temp_node = instruction_word_list_temp_node->_next;
    }

    while (data_array_temp_node) {/*iterating through data table, printing it's words*/
        fprintf(output_file_ob, "  %4X %25s %04X ", data_array_temp_node->_adress," ", data_array_temp_node->_val);/* !!! change to hex */
        fprintf(output_file_ob, "%s", "\n");
        data_array_temp_node = data_array_temp_node->_next;
    }

    fclose(output_file_ob);
    return 1;
}

/* The following function gets an _input_file_name_temp and prints to '_input_file_name_temp.ob' the desired results. returns -1 on error*/
int print_ent_file(char *_input_file_name_temp) {
    char _output_file_name_ent[500] = {'\0'};
    FILE *output_file_ent = NULL;
    symbols_array *symbols_array_temp_node = _symbols_head;

    strcpy(_output_file_name_ent, _input_file_name_temp);
    strcat(_output_file_name_ent, ".ent");/*adding suffix*/

    if ((output_file_ent = fopen(_output_file_name_ent, "w")) == NULL) {/*if opening failed*/
        strcpy(_error_msg, " \nmemory allocation failed - couldn't create file: ");
        strcat(_error_msg, _input_file_name_temp);
        strcat(_error_msg, ".ent");
        return -1;
    }

    while (symbols_array_temp_node) {/*iterating through symbol's table. printing relevant entry labels and their addresses*/
        if (symbols_array_temp_node->_type == 10 || symbols_array_temp_node->_type == 15) {
            fprintf(output_file_ent,"%-10s  %-8X ", symbols_array_temp_node->_val, symbols_array_temp_node->_adress);
            fprintf(output_file_ent,"%s","\n");
        }
        symbols_array_temp_node = symbols_array_temp_node->_next;
    }
    return 1;
}

/* The following function gets an _input_file_name_temp and prints to '_input_file_name_temp.ob' the desired results. returns -1 on error*/
int print_ext_file(char *_input_file_name_temp) {
    temp_label_to_be_placed_list *temp_label_to_be_placed_list_temp_node = temp_lblTBP_head;
    char _output_file_name_ext[500] = {'\0'};
    FILE *output_file_ext = NULL;

    strcpy(_output_file_name_ext, _input_file_name_temp);
    strcat(_output_file_name_ext, ".ext");/*adding suffix*/

    if ((output_file_ext = fopen(_output_file_name_ext, "w")) == NULL) {/*if opening failed*/
        strcpy(_error_msg, "\n memory allocation failed - couldn't create file: ");
        strcat(_error_msg, _input_file_name_temp);
        strcat(_error_msg, ".ext");
        return -1;
    }

    while (temp_label_to_be_placed_list_temp_node) {/*iterating through temp_label to be placed's table. printing relevant extern labels and their addresses*/
        if ((get_label_type(temp_label_to_be_placed_list_temp_node->_label) == 1)) {
            fprintf(output_file_ext,"%-10s  %-8X ", temp_label_to_be_placed_list_temp_node->_label, temp_label_to_be_placed_list_temp_node->_my_address);
            fprintf(output_file_ext,"%s","\n");
        }
        temp_label_to_be_placed_list_temp_node = temp_label_to_be_placed_list_temp_node->_next;
    }
	return 1;
}


/*the following function gets _input_file_name_temp. It is responsible to call the second_cover function from second_cover file.
 * it is responsible to print errors if any are detected. If none were found(neither in the 1st and 2nd cover), it calls the
 * output files print functions.*/
int call_second_cover(char *_input_file_name_temp) {

    if (second_cover_activation() == -1) {/*calls for second cover function from second_cover file*/
        strcpy(_error_msg, "memory allocation failed");
        return -1;
    }

    if (second_cover_err_msg_head != NULL) {/*if any errors are found*/
 	second_cover_err_msg *second_cover_err_msg_temp_node = second_cover_err_msg_head;
        if (_error_mode == 0) {
            _error_mode = -1;
            fprintf(stderr, "%s %s %s","********ERRORS FOUND IN FILE: ",_input_file_name_temp,"*********\n\n");/*header*/
        }
       

        while (second_cover_err_msg_temp_node) {/*print desired errors*/
            fprintf(stderr,"%s %4d %s %s %c", "IN LINE: ",second_cover_err_msg_temp_node->my_line_counter, " MESSAGE: ",second_cover_err_msg_temp_node->err,'\n');/*printing error*/
            second_cover_err_msg_temp_node = second_cover_err_msg_temp_node->_next;
        }
    }

    if (is_symbol_table_valid() == -1) {/*if entry labels were not defined, print desired errors*/
	symbols_array *symbols_array_temp = _symbols_head;
        if (_error_mode == 0) {
            _error_mode = -1;
            fprintf(stderr, "%s %s %s","********ERRORS FOUND IN FILE: ",_input_file_name_temp,"*********\n\n");/*header*/
        }

        

        while (symbols_array_temp) {
            if (symbols_array_temp->_type == 0) {
                fprintf(stderr,"%s %4d %s %s %c", "IN LINE: ",symbols_array_temp->_my_line_counter, " MESSAGE: label of type entry was never defined. Label: ",symbols_array_temp->_val,'\n');/*printing error*/
            }
            symbols_array_temp = symbols_array_temp->_next;
        }
    }

    if (_error_mode == 0) {/*if no errors were encountered during the 1st and 2nd covers, print output files*/
        if (print_ob_file(_input_file_name_temp) == -1) {
            return -1;
        }
        if (print_ent_file(_input_file_name_temp) == -1) {
            return -1;
        }
        if (print_ext_file(_input_file_name_temp) == -1) {
            return -1;
        }
    }
    return 1;
}

/*The following function resets all variables to further iterations*/
void reset_all_related_variables(void) {
    _ic = _IC_INITIALIZED_ADDRESS;
    _dc = _DC_INITIALIZED_VAL;
    _line_counter = 0;
    _error_mode = 0;
    instruction_table_size = 0;
    words_TBP_table_size = 0;
    data_table_size = 0;

    reset_directive_list();
    reset_instruction_list();
    reset_label_list();
    reset_TBP_list();
    reset_second_cover_errors_list();
    memset(_error_msg, '\0', sizeof(_error_msg));
}
