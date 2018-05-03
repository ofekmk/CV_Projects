
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include "second_cover.h"
#include "directive_cmd_data_structure.h"
#include "instruction_data_structure.h"
#include "label_data_structure.h"
#include "add_to_list_macros.h"
#define ERROR_MSG_SIZE 500/*error message size*/

extern char _error_msg[ERROR_MSG_SIZE];/*error message holder*/
extern int _ic;/*instruction table*/
extern int _line_counter;/*line position in file*/
extern int instruction_table_size;/*size of instruction table*/
extern int words_TBP_table_size;/*size of words to be placed table*/


/*head and tail of second_cover_err_msg table*/
second_cover_err_msg *second_cover_err_msg_head = NULL;
second_cover_err_msg *second_cover_err_msg_tail = NULL;

/*head and tail of temp_label_to_be_placed_list table*/
temp_label_to_be_placed_list *temp_lblTBP_head = NULL;
temp_label_to_be_placed_list *temp_lblTBP_tail = NULL;


/* function declaration*/
int add_second_cover_error_msg_to_list(char *error_message, int input_line_counter);


/* The following function adds an add_to_tmp_lbl_TBP_list word to the list. returns -1 on memory allocation failure*/
int add_to_tmp_lbl_TBP_list(char *label) {
    temp_label_to_be_placed_list *_node = NULL;
    NEW_NODE(temp_label_to_be_placed_list, _node);

    if (_node == NULL) {

        return -1;/*allfailed*/
    }

    if (temp_lblTBP_head == NULL) {/*adding to list if list is empty*/
        ADD_TO_LIST_IF_HEAD_NULL(temp_lblTBP_head, _node);/*calling related macro from add_to_list_macros.h*/
        _node->_my_address = _ic++;
        strcpy(_node->_label, label);
        _node->my_line_counter = _line_counter;

    } else if (temp_lblTBP_tail == NULL) {/*adding to list if list contains one argument*/
        ADD_TO_LIST_IF_TAIL_NULL(temp_lblTBP_head, temp_lblTBP_tail, _node);/*calling related macro from add_to_list_macros.h*/
        strcpy(_node->_label, label);
        _node->my_line_counter = _line_counter;
        _node->_my_address = _ic++;

    } else {/*adding to list if it contains 2 arguments or more*/
        ADD_TO_LIST_ELSE(temp_lblTBP_tail, _node);/*calling related macro from add_to_list_macros.h*/
        _node->_my_address = _ic++;
        strcpy(_node->_label, label);
        _node->my_line_counter = _line_counter;
    }
    return 1;
}


/*The following iterates through words to be placed table, and puts them in their right position in instruction table. -1 is returned on error*/
int second_cover_activation(void) {
    int get_defined_label_address;/*holds the address of the defined label*/
    int get_defined_label_type;/*holds the type of the dfined label*/
    char _inner_error_msg[ERROR_MSG_SIZE] = {'\0'};/*error message holder*/
    temp_label_to_be_placed_list *temp = temp_lblTBP_head;

    update_directive_labels();/*update_directive_labels function updates the directive label's in the 2nd cover*/

    /*update_directive_table function iterates through the data's table and updates every node's address to it's new relevant address, after the
 * instruction table is all set. each node's address will be equaled to it's given _dc value,
 * plus the last instruction's word's address(which is 100+the length of(instruction_table and words_to_be_placed table).
 * */
    update_directive_table();

    while (temp) {/*while not end of words to be placed table*/
        if ((get_defined_label_type = get_label_type(temp->_label)) == -1) {/*if the label was never defined*/
            strcpy(_inner_error_msg, "label is used but was never defined . label's name: ");
            strcat(_inner_error_msg, temp->_label);
            if (add_second_cover_error_msg_to_list(_inner_error_msg, temp->my_line_counter) == -1) {
                strcpy(_inner_error_msg, "memory allocation failed ");
                return -1;
            }

        } else if (get_defined_label_type == 0 || get_defined_label_type == -1) {/*if label is entry, but was never defined*/
            strcpy(_inner_error_msg, "label in instruction command is type of entry, but the label was never defined. label: ");
            strcat(_inner_error_msg, temp->_label);
            if (add_second_cover_error_msg_to_list(_inner_error_msg, temp->my_line_counter) == -1) {
                strcpy(_inner_error_msg, "memory allocation failed ");
                return -1;
            }

        } else {/*add the word to instruction table*/
            get_defined_label_address = get_label_address(temp->_label);
           if( add_second_cover_label_to_instruction_table(temp->_my_address, get_defined_label_type, get_defined_label_address)==-1){
               return -1;
           }
        }
        temp = temp->_next;
    }
    return 1;
}


/* The following function adds an second_cover_err_msg node to the list. returns -1 on memory allocation failure*/
int add_second_cover_error_msg_to_list(char *error_message, int input_line_counter) {
    second_cover_err_msg *_node = NULL;

    NEW_NODE(second_cover_err_msg, _node);

    if (_node == NULL) {
        return -1;/*allfailed*/
    }

    if (second_cover_err_msg_head == NULL) {/*adding to list if list is empty*/
        ADD_TO_LIST_IF_HEAD_NULL(second_cover_err_msg_head, _node);/*calling related macro from add_to_list_macros.h*/
        strcpy(_node->err, error_message);
        _node->my_line_counter = input_line_counter;

    } else if (second_cover_err_msg_tail == NULL) {/*adding to list if list contains one argument*/
        ADD_TO_LIST_IF_TAIL_NULL(second_cover_err_msg_head, second_cover_err_msg_tail, _node)/*calling related macro from add_to_list_macros.h*/
        strcpy(_node->err, error_message);
        _node->my_line_counter = input_line_counter;

    } else {/*adding to list if it contains 2 arguments or more*/
        ADD_TO_LIST_ELSE(second_cover_err_msg_tail, _node);/*calling related macro from add_to_list_macros.h*/
        strcpy(_node->err, error_message);
        _node->my_line_counter = input_line_counter;
    }
    return 1;
}

/*gets the 'words to be placed' table size*/
int compute_TBP_table_size(void) {
    int size = 0;
    temp_label_to_be_placed_list *node = temp_lblTBP_head;

    while (node) {
        size++;
        node = node->_next;
    }
    return size;
}

/*resets 'words to be placed' table*/
void reset_TBP_list(void) {
    temp_label_to_be_placed_list *node1 = temp_lblTBP_head;
    temp_label_to_be_placed_list *node2 = NULL;

    while (node1) {
        node2 = node1->_next;
        free(node1);
        node1 = node2;
    }

    temp_lblTBP_head = NULL;
    temp_lblTBP_tail = NULL;
}

void reset_second_cover_errors_list(void) {
    second_cover_err_msg *node1 = second_cover_err_msg_head;
    second_cover_err_msg *node2 = NULL;

    while (node1) {
        node2 = node1->_next;
        free(node1);
        node1 = node2;
    }

    second_cover_err_msg_head = NULL;
    second_cover_err_msg_tail = NULL;
}


