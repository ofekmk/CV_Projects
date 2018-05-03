
#include "directive_line_examiner.h"
#include <string.h>
#include <stdio.h>
#include <ctype.h>
#include "first_cover.h"

#define ERROR_MSG_SIZE 500/* defined length of error message*/
#define WORD_SIZE 15

extern int _error_mode;
extern char _error_msg[ERROR_MSG_SIZE];/* variable that holds the error message(in case there is one..)*/


/*The following function gets the inspected line '_line', the line's current position
 * '_line_pos'.The function goes through the line from the given position, and puts the .data line numbers into
 * the array '_temp_numbers_arr'. return value - '_temp_numbers_arr' array's length . -1 on error. */
int data_line_interpreter(char *_line, int _line_pos, int *_temp_numbers_arr) {
    int data_numbers_array_length = 0;/*stores the length of the '_temp_numbers_arr' array*/
    int current_char; /*temporary variable to store characters*/
    short temp_number;/* temporary variable to store a line's number*/

    while (isspace(current_char = _line[_line_pos]) || current_char == '\0' || current_char == EOF) {/*ignoring white spaces,  return error if no arguments given*/
        if (current_char == '\0' || current_char == '\n' || current_char == EOF) {
            strcpy(_error_msg, ".data  line cannot be empty");
            return -1;
        }
        _line_pos++;
    }

    while ((analyze_number(_line, &_line_pos, &temp_number)) == 1) {/*analyze a .data line number. return -1 on error*/


        if (temp_number < 0) {/*toggle msb bit*/
            temp_number ^= 1 << WORD_SIZE;
        }

        _temp_numbers_arr[data_numbers_array_length++] = temp_number;/*add the validated number to the array*/

        while (isspace(current_char = _line[_line_pos]) || current_char == '\0' || current_char == EOF) {/*ignoring white spaces. return if no more aerguments*/
            if (current_char == '\0' || current_char == '\n' || current_char == EOF) {
                return data_numbers_array_length;
            }
            _line_pos++;
        }
        if (current_char != ',') { /*if argument was found, but not a comma*/
            strcpy(_error_msg, "bad syntax in .data line(only numbers allowed, separated by commas");
            return -1;

        } else {
            _line_pos++;

        }
        while (isspace(current_char = _line[_line_pos]) || current_char == '\0' || current_char == EOF) {/*looking for the ext number after comma*/
            if (current_char == '\0' || current_char == '\n' || current_char == EOF) {
                strcpy(_error_msg, "missing argument after comma in .data line");/*return error if no argument is given after comma*/
                return -1;
            }
            _line_pos++;
        }
    }
 return -1;
}


/*The following function gets the inspected line '_line', its line position '_line_pos'.
 * The function checks if the given argument '_current_arg' is a valid string, as a .string line argument.*/
int trim_string(char *_line, int _line_pos, char *_current_arg) {
    int current_char;/*temporary variable to store characters*/
    int _current_arg_pos = 0;/*temporary variable to store line position*/

    while (isspace(current_char = _line[_line_pos]) || current_char == '\0' || current_char == EOF) {/*empty .string line will result in error*/
        if (current_char == '\0' || current_char == '\n' || current_char == EOF) {
            strcpy(_error_msg, ".string  line cannot be empty");
            return -1;
        }
        _line_pos++;
    }

    if (current_char != '\"') {/* error is returned if the first quotation mark is missing*/
        strcpy(_error_msg, "Missing first quotation mark int .string command");
        return -1;
    }

    _line_pos++;
    current_char = _line[_line_pos];

    if (current_char == '\"') {/*empty .string line will result in error*/
        strcpy(_error_msg, "string cannot be empty in .string command");
        return -1;
    }

    while (current_char != '\"') {/* error is returned if the second quotation mark is missing*/
        _current_arg[_current_arg_pos++] = current_char;
        _line_pos++;
        current_char = _line[_line_pos];
        if (current_char == '\n' || current_char == '\0' || current_char == EOF) {
            strcpy(_error_msg, "string must end with quotation mark");
            return -1;
        }
    }

    _line_pos++;

    while (isspace(current_char = _line[_line_pos]) || current_char == '\0' || current_char == EOF) {
        if (current_char == '\0' || current_char == '\n' || current_char == EOF) {
            return 1;
        }
        _line_pos++;
    }

    strcpy(_error_msg, "only white space permitted after second quotation mark in .string command");
    return -1;/* error is returned if non whitespaces characters appear after the second quotation mark is missing*/
}


/*The following function gets the inspected .entry\.extern line '_line', its line position '_line_pos'.
 * The function returns 1 if the .entry\.extern line contains a valid label's name, and guards it in '_current_arg'. Returns -1 on error*/
int get_val_of_entry_or_ext(char *_line, char *_current_arg, int _line_pos) {
    int current_char;/*temporary variable to store characters*/
    int _current_arg_pos = 0;/*temporary variable to store line position*/

    while (isspace(current_char = _line[_line_pos])) {/*empty .entry/.extern line results in error*/
        if (current_char == '\0' || current_char == EOF || current_char == '\n') {
            strcpy(_error_msg, "missing argument in given directive command(1 argument as label is expected for .entry/.extern command)");
            return -1;
        }
        _line_pos++;
    }

    while (!(isspace(current_char)) && !(current_char == '\0') && !(current_char == EOF)) {/*get next line's argument*/
        _current_arg[_current_arg_pos++] = current_char;
        _line_pos++;
        current_char = _line[_line_pos];
    }
    while (isspace(_line[_line_pos])) {/* ignoring further whitespaces characters*/
        _line_pos++;
    }

    if (_line[_line_pos] != '\0' && _line[_line_pos] != EOF) {/* return error if more than one argument is given*/
        strcpy(_error_msg, "Too many arguments given in directive command(only 1 argument as label is expected for .entry/.extern command)");
        return -1;
    }

    _current_arg[strlen(_current_arg)] = ':';
    if (is_label(_current_arg) == -1) {/*calling 'is_label' function to check if label is valid*/
        strcpy(_error_msg, "incorrect label syntax in given directive command");
        return -1;
    }

    _current_arg[strlen(_current_arg) - 1] = '\0';
    return 1;
}
