
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "directive_cmd_data_structure.h"
#include "add_to_list_macros.h"

extern int _dc;/* data counter*/
extern int _ic;/* instruction counter*/
extern char _error_msg[500]; /*error message holder*/
extern int _error_mode;
extern int instruction_table_size; /*size of instruction words list*/
extern int words_TBP_table_size; /*size of list that contains the words to be placed(TBP) in the second cover process*/

data_array *_data_array_head = NULL; /*head pointer of the data's list*/
data_array *_data_array_tail = NULL; /*tail pointer of the data's list*/



/* The following function gets an array of numbers ,'*nums_arr',and it's length, 'arr_length', and
 * adds the array's numbers to the data table, number by number. returns -1 on allocation error, else 1*/
int add_numbers_to_data_array(int *nums_arr, int arr_length) {
    int i;/*loop counter*/
    data_array *_node = NULL;/*data list's node*/

    for (i = 0; i < arr_length; i++) {
        NEW_NODE(data_array, _node);/*calling the allocation macro*/

        if (_node == NULL) {

            return -1;/*allocation failed*/
        }

        if (_data_array_head == NULL) {/*adding to list if list is empty*/
            ADD_TO_LIST_IF_HEAD_NULL(_data_array_head, _node);/*calling related macro from add_to_list_macros.h*/
            _node->_adress = _dc++;
            _node->_val = nums_arr[i];

        } else if (_data_array_tail == NULL) {/*adding to list if list contains one argument*/
            ADD_TO_LIST_IF_TAIL_NULL(_data_array_head, _data_array_tail, _node);/*calling related macro from add_to_list_macros.h*/
            _node->_val = nums_arr[i];
            _node->_adress = _dc++;

        } else {/*adding to list if it contains 2 arguments or more*/
            ADD_TO_LIST_ELSE(_data_array_tail, _node);/*calling related macro from add_to_list_macros.h*/
            _node->_adress = _dc++;
            _node->_val = nums_arr[i];
        }
    }

    return 1; /*adding succeeded*/
}


/*The following function computes the size of the data's table*/
int compute_data_table_size(void) {
    int size = 0;/* return value*/
    data_array *node = _data_array_head;

    while (node) {/*Going through all table's values*/
        size++;
        node = node->_next;
    }
    return size;
}


/*The following function gets '_string' as a '.string' command argument,
 * and adding the string's characters one by one to data's table. zero value will
 * be placed as last character*/
int add_string_to_data_array(char *_string) {
    int i = 0;
    int _temp_arr[81] = {0};/*temporary string holder*/
    int _arr_length = strlen(_string);/* length of the given string*/

    for (i = 0; i < _arr_length; i++) {
        _temp_arr[i] = _string[i];
    }

    _temp_arr[_arr_length + 1] = 0;
    if (add_numbers_to_data_array(_temp_arr, _arr_length + 1) == -1){/*adding string characters to data's table using 'add_numbers_to_data_array' function*/
    return -1;
    }
 return 1;
}


/*The following function resets the data table to an empty table*/
void reset_directive_list(void) {
    data_array *node1 = _data_array_head;/*node that holds the table's argument*/
    data_array *node2 = NULL;/*temp node that guards the next argument*/

    while (node1) {/*freeing all nodes*/
        node2 = node1->_next;
        free(node1);
        node1 = node2;
    }

    _data_array_head = NULL;/* initializing list's head and tail to NULL*/
    _data_array_tail = NULL;
}


/*The following function iterates through the data's table and updates every node's address to it's new relevant address, after the
 * instruction table is all set. each node's address will be equaled to it's given _dc value,
 * plus the last instruction's word's address(which is 100+the length of(instruction_table and words_to_be_placed table).
 * */
void update_directive_table(void) {
    data_array *node = _data_array_head;

    while (node) {
        node->_adress = 100 + instruction_table_size + words_TBP_table_size + node->_adress;
        node = node->_next;
    }
}
