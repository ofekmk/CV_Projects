
#include <stdlib.h>
#include <string.h>
#include "label_data_structure.h"
#include "add_to_list_macros.h"

#define ERROR_MSG_SIZE 500 /*length of error message*/

extern int _dc;/*data counter*/
extern int _ic;/*instruction counter*/
extern char _error_msg[ERROR_MSG_SIZE];/*error message holder*/
extern int _error_mode;
extern int instruction_table_size;/*size of instruction table*/
extern int words_TBP_table_size;/*size of words_to_be_positioned_table*/
extern int _line_counter;/*current line in file*/

symbols_array *_symbols_head = NULL;/*list head*/
symbols_array *_symbols_tail = NULL;/*list tail*/



/*the following function gets a label and sets it's type*/
void set_directive_label_typee(char *label) {
    symbols_array *node = _symbols_head;

    while (node) {
        if (strcmp(node->_val, label) == 0) {
            node->_adress = _dc;
            if ((node->_type) == 10) {
                node->_type = 15;
            } else {
                node->_type = 5;
            }
            break;
        }
        node = node->_next;
    }


}


/*the following function gets a label and a type and sets it's type*/
void set_label_type(char *label, int type) {
    symbols_array *node = _symbols_head;

    while (node) {
        if (strcmp(node->_val, label) == 0) {
            node->_type = type;
            break;
        }
        node = node->_next;
    }
}

/*searching for a label _arg in symbols table. return -1 if not found.*/
int search_label(char *_arg) {
    symbols_array *node = _symbols_head;
    while (node) {
        if (strcmp(node->_val, _arg) == 0) {
            return node->_type;
        }
        node = node->_next;
    }
    return -1;
}


/* The following function adds an instruction word to the list. returns -1 on memory allocation failure*/
int add_label_to_symbols_array(char *label, int type) {
    symbols_array *_node = NULL;

    NEW_NODE(symbols_array, _node);/*list's node*/
    if (_node == NULL) {
        return -1;/*allocation failed*/
    }

    if (_symbols_head == NULL) {/*adding to list if list is empty*/
        ADD_TO_LIST_IF_HEAD_NULL(_symbols_head, _node);/*calling related macro from add_to_list_macros.h*/
        _node->_adress = _ic;
        _node->_type = type;
        strcpy(_node->_val, label);
        _node->_my_line_counter = _line_counter;

    } else if (_symbols_tail == NULL) {/*adding to list if list contains one argument*/
        ADD_TO_LIST_IF_TAIL_NULL(_symbols_head, _symbols_tail, _node)/*calling related macro from add_to_list_macros.h*/
        strcpy(_node->_val, label);
        _node->_my_line_counter = _line_counter;
        _node->_adress = _ic;
        _node->_type = type;

    } else {/*adding to list if it contains 2 arguments or more*/
        ADD_TO_LIST_ELSE(_symbols_tail, _node);/*calling related macro from add_to_list_macros.h*/
        _node->_adress = _ic;
        _node->_type = type;
        strcpy(_node->_val, label);
        _node->_my_line_counter = _line_counter;
    }
    return 1;/*adding succeeded*/
}



/*the function returns the label's address(of label_to_search)*/
int get_label_address(char *label_to_search) {
    symbols_array *temp = _symbols_head;
    while (temp) {
        if (strcmp(temp->_val, label_to_search) == 0) {
            return temp->_adress;
        }
        temp = temp->_next;
    }
    return -1;
}


/*This function returns label_to_search's type*/
int get_label_type(char *label_to_search) {
    symbols_array *temp = _symbols_head;

    while (temp) {
        if (strcmp(temp->_val, label_to_search) == 0) {
            return temp->_type;
        }
        temp = temp->_next;
    }
    return -1;
}

/*resets the symbols list*/
void reset_label_list(void) {

    symbols_array *node1 = _symbols_head;
    symbols_array *node2 = NULL;
    while (node1) {
        node2 = node1->_next;
        free(node1);
        node1 = node2;

    }
    _symbols_head = NULL;
    _symbols_tail = NULL;
}

/*The function updates the directive label's in the 2nd cover*/
void update_directive_labels() {
    symbols_array *temp = _symbols_head;

    while (temp) {
        if (temp->_type == 5 || temp->_type == 15) {
            temp->_adress = 100 + instruction_table_size + words_TBP_table_size + temp->_adress;
        }
        temp = temp->_next;
    }
}

/*The function updates the .entry label if it's definition is found*/
void update_entry_label_address(char *label) {
    symbols_array *temp = _symbols_head;

    while (temp) {
        if (strcmp(temp->_val, label) == 0) {
            temp->_adress = _ic;
        }
        temp = temp->_next;
    }
}


/*removing a node from the list*/
void remove_label_node_from_symbol_table(char *label) {

    symbols_array *temp = _symbols_head;
    symbols_array *temp2 = _symbols_head;

    if (_symbols_head == NULL) { /*if list is empty*/
        return;
    }

    if (_symbols_tail == NULL) { /*if i am the head, and the tai is null*/
        if (strcmp(label, temp->_val) == 0) {
            free(temp);
            _symbols_head = NULL;
        }
        return;
    }

    if (strcmp(label, temp->_val) == 0) { /*if i am the head, and the next arg is tail*/
        _symbols_head = _symbols_head->_next;
        if (_symbols_head->_next == NULL) {
            _symbols_tail = NULL;
        }
        return;
    }
    temp = temp->_next; /*temp2 has the previous argument, which in this current situation it is symbols_head*/

    while (temp) {/* the arg is for sure in between head and tail(not include), and head->next != tail*/
        if (strcmp(label, temp->_val) == 0) {
            if (temp->_next == NULL) {/* if we reached the tail*/
                temp2->_next = NULL; 
                if(temp2==_symbols_head){
                  _symbols_tail=NULL;
                }
                else{
                   _symbols_tail = temp2;
                }
                
                free(temp);
                return;

            } else {/*were not at the tail*/
                temp2->_next = temp->_next;
                free(temp);
                return;
            }
        }
        temp = temp->_next;
        temp2 = temp2->_next;
    }
}


/*checks if there's an entry label that was not defined*/
int is_symbol_table_valid(void) {
    symbols_array *temp = _symbols_head;

    while (temp) {
        if (temp->_type == 0) {
            return -1;
        }
        temp = temp->_next;
    }
    return 1;
}
