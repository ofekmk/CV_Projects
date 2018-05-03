

#ifndef UNTITLED_INSTRUCTION_LINE_CLASSIFICATION_H
#define UNTITLED_INSTRUCTION_LINE_CLASSIFICATION_H

/*The following function gets the inspected line '_line', its line position '_line_pos', and and operation code '_op_code'.
 * The functions checks for how many operands the command is needed to, and calls the desired function*/
int command_line_examiner(char* _line, int _line_pos,int _op_code);

/*The following function is responsible to give the 'converter' function the necessary values to get an additional word, with designation number 0 or 1.
 * it gets era\val values to determine the word's bits*/
short convert_additional_word_type0_or_type1_to_bits(int are,short val);

#endif 
