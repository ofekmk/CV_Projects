

#ifndef UNTITLED_DIRECTIVE_LINES_EXAMINER_H
#define UNTITLED_DIRECTIVE_LINES_EXAMINER_H

/*trim_string function gets the inspected line '_line', its line position '_line_pos'.
 * The function checks if the given argument '_current_arg' is a valid string, as a .string line argument.*/
int trim_string(char* _line,int _line_pos,char* _current_arg);

/*data_line_interpreter gets the inspected line '_line', the line's current position
 * '_line_pos'.The function goes through the line from the given position, and puts the .data line numbers into
 * the array '_temp_numbers_arr'. return value - '_temp_numbers_arr' array's length . -1 on error. */
int data_line_interpreter(char* _line,int _line_pos, int* _temp_numbers_arr);

/*get_val_of_entry_or_ext gets the inspected .entry\.extern line '_line', its line position '_line_pos'.
 * The function returns 1 if the .entry\.extern line contains a valid label's name, and guards it in '_current_arg'. Returns -1 on error*/
int get_val_of_entry_or_ext(char* _line, char* _current_arg,int _line_pos);
#endif 
