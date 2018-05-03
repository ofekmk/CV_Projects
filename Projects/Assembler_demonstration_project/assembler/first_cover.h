

#ifndef UNTITLED_FIRST_COVER_H
#define UNTITLED_FIRST_COVER_H

/* first_cover is called from main after getting a line from the file. The function is the "headquarter" of most of the program's functions.
 * It trims the line, and delivers the desired arguments to the responsible functions. It checks if line is empty, or if it holds a comment.
 * If none of the above is found, it calls for is_label function to check if there is a label in the beginning of the line.
 * It checks if it has a directive or instruction commands, and deliver it's arguments to the caring functions. The function also deters if the command is not recognized.
 * The function will return the first encountered error in any of the steps,if error is found. The function will return value 1 if the line is valid, back to the main function.*/
int  first_cover(char * _line);

/* The following function gets an argument '_current_arg' and deters if the argument is a valid label. returns -1 on error*/
int is_label(char* _current_arg);

/*he following function gets the inspected line '_line', its line position '_line_pos'. It stores inside 'num' a validated number. returns -1 on error*/
short analyze_number(char* _line, int* _line_pos, short* num);

#endif 
