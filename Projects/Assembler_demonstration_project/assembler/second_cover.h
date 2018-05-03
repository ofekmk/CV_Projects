

#ifndef UNTITLED_SECOND_COVER_H
#define UNTITLED_SECOND_COVER_H
typedef struct  second_cover_err_msg{char err[500];int my_line_counter; struct second_cover_err_msg* _next;}second_cover_err_msg;
typedef struct temp_label_to_be_placed_list{unsigned short _my_address; char _label[31]; int my_line_counter  ;struct temp_label_to_be_placed_list* _next;}temp_label_to_be_placed_list;


/*resets 'words to be placed' table*/
void reset_TBP_list(void);

/*gets the 'words to be placed' table size*/
int compute_TBP_table_size(void);

/* The following function adds an add_to_tmp_lbl_TBP_list word to the list. returns -1 on memory allocation failure*/
int add_to_tmp_lbl_TBP_list(char * label);

/*The following iterates through words to be placed table, and puts them in their right position in instruction table. -1 is returned on error*/
int second_cover_activation(void);

/*resets 'reset_second_cover_errors_list' table*/
void reset_second_cover_errors_list(void);
#endif 
