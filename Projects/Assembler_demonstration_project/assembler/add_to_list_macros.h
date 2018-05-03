

#ifndef UNTITLED_ADD_TO_LIST_MACROS_H
#define UNTITLED_ADD_TO_LIST_MACROS_H
/*This header contains macros for the various data structures*/


#define NEW_NODE(TYPE,NODE)   NODE = (TYPE*) calloc (1,sizeof(TYPE));/*macro that initializes a new list node*/

#define ADD_TO_LIST_IF_HEAD_NULL(HEAD,NODE) HEAD = NODE; HEAD->_next = NULL;/* macro that is activared when the list is empty*/
#define ADD_TO_LIST_IF_TAIL_NULL(HEAD,TAIL,NODE) TAIL=NODE; NODE->_next=NULL; HEAD->_next=NODE; /*macro that is activated when the list contains one argument*/
#define ADD_TO_LIST_ELSE(TAIL,NODE) TAIL->_next=NODE; TAIL=NODE/* macro that is activated when there are 2 or more arguments in the list*/


#endif 
