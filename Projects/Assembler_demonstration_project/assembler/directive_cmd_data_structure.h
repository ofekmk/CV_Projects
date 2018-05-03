

#ifndef UNTITLED_DIRECTIVE_CMD_DATA_STRUCTURE_H
#define UNTITLED_DIRECTIVE_CMD_DATA_STRUCTURE_H


struct data_array{
    unsigned short _adress;/*stores the _dc value.*/
    short _val; /*stores the argument(number or char).*/
    struct data_array* _next;/*points to the next argument in the list*/
};typedef struct data_array data_array;


/*update_directive_table function iterates through the data's table and updates every node's address to it's new relevant address, after the
 * instruction table is all set. each node's address will be equaled to it's given _dc value,
 * plus the last instruction's word's address(which is 100+the length of(instruction_table and words_to_be_placed table).
 * */
void update_directive_table(void);

/* compute_data_table_size function computes the size of the data's table*/
int compute_data_table_size(void);

/*reset_directive_list function resets the data table to an empty table*/
void reset_directive_list(void);

/* add_numbers_to_data_array function gets an array of numbers ,'*nums_arr',and it's length, 'arr_length', and
 * adds the array's numbers to the data table, number by number. returns -1 on allocation error, else 1*/
int add_numbers_to_data_array(int * _data_num_arr, int data_arr_length);

/*add_string_to_data_array function gets '_string' as a '.string' command argument,
 * and adding the string's characters one by one to data's table. zero value will
 * be placed as last character*/
int add_string_to_data_array(char *_string);

#endif 
