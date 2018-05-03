

#include "label_data_structure.h"
#include <string.h>
#include "instruction_data_structure.h"
#include "instruction_line_examiner.h"
#include <stdlib.h>
#include "directive_cmd_data_structure.h"
#include "add_to_list_macros.h"

#define ERROR_MSG_SIZE 500/* error message maximum's length*/

extern char _error_msg[ERROR_MSG_SIZE];/*error message holder*/
extern int _ic;/*instruction counter*/
extern int _dc;/*data counter*/

instruction_word_list *instruction_word_list_head = NULL;/*instruction_word_list head*/
instruction_word_list *instruction_word_list_tail = NULL;/*instruction_word_list tail*/



/* The following function adds an instruction word to the list. returns -1 on memory allocation failure*/
int add_to_instruction_array(short instruction_word) {
    instruction_word_list *_node = NULL;
    NEW_NODE(instruction_word_list, _node);/*list's node*/

    if (_node == NULL) {
        return -1;/*allocation failed*/
    }

    if (instruction_word_list_head == NULL) {/*adding to list if list is empty*/
        ADD_TO_LIST_IF_HEAD_NULL(instruction_word_list_head, _node);/*calling related macro from add_to_list_macros.h*/
        _node->_instruction_word = instruction_word;
        _node->_address = _ic++;

    } else if (instruction_word_list_tail == NULL) {/*adding to list if list contains one argument*/
        ADD_TO_LIST_IF_TAIL_NULL(instruction_word_list_head, instruction_word_list_tail, _node);/*calling related macro from add_to_list_macros.h*/
        _node->_instruction_word = instruction_word;
        _node->_address = _ic++;

    } else {/*adding to list if it contains 2 arguments or more*/
        ADD_TO_LIST_ELSE(instruction_word_list_tail, _node);/*calling related macro from add_to_list_macros.h*/
        _node->_instruction_word = instruction_word;
        _node->_address = _ic++;
    }
    return 1;/*adding succeeded*/
}


/* The following function(activated in 2nd cover), adds to instruction table a given additional word from to_be_placed_table,
 * to 'add_to_address' position in instruction's table, with the 'genuine_label_address', and it's ARE type(derived from 'label_type').
 *
 * e.g: add_to_address=104 label_type=regular_declaration,genuine_label_address=120: the additional word's label address is 120,
 * the word's position in inst.table is 104, and the word's ARE is 0(because its type declaration is regular)*/
int add_second_cover_label_to_instruction_table(int add_to_address, int label_type, int genuine_label_address) {
    int are;
    instruction_word_list *temp = instruction_word_list_head;/* the instruction's node positioned to head */
    instruction_word_list *add_new_node = NEW_NODE(instruction_word_list, add_new_node);/*the node to be added*/
    
    if(add_new_node==NULL){
        strcpy(_error_msg,"memory allocation failed");
        return -1;
    }

    
    /* setting ARE value*/
    if (label_type == 1) {
        are = 1;

    } else {
        are = 2;
    }

    while (temp) {/*looking for the right place to put the word, according to 'add_to_address*/
        if (temp->_address == add_to_address - 1) {
            add_new_node->_next = temp->_next;
            temp->_next = add_new_node;
            add_new_node->_address = add_to_address;

            if (label_type != 1) {/*add the label's address with the ARE value*/
                add_new_node->_instruction_word = convert_additional_word_type0_or_type1_to_bits(are, genuine_label_address);

            } else {/*if the label is an external type, add an address of 0*/
                add_new_node->_instruction_word = convert_additional_word_type0_or_type1_to_bits(are, 0);
            }
            break;
        }
        temp = temp->_next;
    }
    return 1;
}


/*The following function resets the instruction table*/
void reset_instruction_list(void) {
    instruction_word_list *node1 = instruction_word_list_head;
    instruction_word_list *node2 = NULL;

    while (node1) {
        node2 = node1->_next;
        free(node1);
        node1 = node2;
    }

    instruction_word_list_head = NULL;
    instruction_word_list_tail = NULL;
}


/*the function returns the size of the instruction's table*/
int compute_instruction_table_size(void) {
    int size = 0;
    instruction_word_list *node = instruction_word_list_head;

    while (node) {
        size++;
        node = node->_next;
    }
    return size;
}




