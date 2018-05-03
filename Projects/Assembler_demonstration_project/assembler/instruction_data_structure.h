

#ifndef UNTITLED_DATA_STRUCTURE_OPS_H
#define UNTITLED_DATA_STRUCTURE_OPS_H



/* The instruction table contains nodes with the following: address in the table, the word, and the next list's value*/
typedef struct instruction_word_list {int _address ;short _instruction_word;struct instruction_word_list* _next;}instruction_word_list;

/*the function returns the size of the instruction's table*/
int compute_instruction_table_size(void);

/*The following function resets the instruction table*/
void reset_instruction_list(void);

/* The following function adds an instruction word to the list. returns -1 on memory allocation failure*/
int add_to_instruction_array(short instruction_word);

/* The following function(activated in the 2nd cover process), adds to instruction table a given additional word from to_be_placed_table,
 * to 'add_to_address' position in instruction's table, with the 'genuine_label_address', and it's ARE type(derived from 'label_type').
 *
 * e.g: add_to_address=104 label_type=regular_declaration,genuine_label_address=120: the additional word's label address is 120,
 * the word's position in inst.table is 104, and the word's ARE is 0(because its type declaration is regular)*/
int add_second_cover_label_to_instruction_table(int add_to_address,int label_type,int genuine_label_address);
#endif 
