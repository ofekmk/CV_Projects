

#ifndef UNTITLED_LABEL_DATA_STRUCTURE_H
#define UNTITLED_LABEL_DATA_STRUCTURE_H

/*type of symbol's node variable*/
struct symbols_array{
    char  _val[31];/*value*/
    short _adress;/*address*/
    short _type;/*label's type*/
    int _my_line_counter;/*line number where the label was defined*/
    struct symbols_array* _next;/*next node*/
};typedef struct symbols_array symbols_array;


/*checks if there's an entry label that was not defined*/
int is_symbol_table_valid(void);

/*removing a node from the list*/
void remove_label_node_from_symbol_table(char * label);

/*The function updates the .entry label if it's definition is found*/
void update_entry_label_address(char* label);

/*The function updates the directive label's in the 2nd cover*/
void update_directive_labels();

/*resets the symbols list*/
void reset_label_list(void);

/*the following function gets a label and a type and sets it's type*/
void set_label_type(char * label,int type);


/*the following function gets a label and sets it's type*/
void set_directive_label_typee(char * label);

/*the function returns the label's address(of label_to_search)*/
int get_label_address(char * label_to_search);

/*This function returns label_to_search's type*/
int get_label_type(char * label_to_search);

/*searching for a label _arg in symbols table. return -1 if not found.*/
int search_label(char* _arg);

/* The following function adds an instruction word to the list. returns -1 on memory allocation failure*/
int add_label_to_symbols_array(char * label,int type);

#endif 
