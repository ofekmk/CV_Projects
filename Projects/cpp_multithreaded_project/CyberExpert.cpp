#include "../include/CyberExpert.h"
#include <stdlib.h>     /* include for abs */
using namespace std;

    CyberExpert::CyberExpert(std::string cyber_expert_name_, int cyber_expert_work_time_,
    		int cyber_expert_rest_time_, int cyber_expert_efficiency_):
    	cyber_expert_name_(cyber_expert_name_),cyber_expert_work_time_(cyber_expert_work_time_),
		cyber_expert_rest_time_(cyber_expert_rest_time_ ),cyber_expert_efficiency_(cyber_expert_efficiency_)
    {
    	isActive= cyber_expert_work_time_;
    }



    void CyberExpert::Clean(CyberPC & cyber_pc){
 	 cyber_pc.Disinfect();
    }

    int CyberExpert::getIsActive()
    {
    	return isActive;
    }

    const std::string CyberExpert::getName()
    {
    	return cyber_expert_name_;
    }

    int CyberExpert::getEfficiency()
    {
    	return cyber_expert_efficiency_;
    }



    void CyberExpert::setIsActive(int num)
       {

    	if (num==0){
           	cout<< cyber_expert_name_ << " is taking a break" << endl;
           	isActive = num;
    	}
       	else
       	if(num<0)
       	{
       		if (abs(num) == (cyber_expert_rest_time_)){
       			isActive =cyber_expert_work_time_;
       			cout<< cyber_expert_name_ << " is back to work" << endl;
       		}
       		else
       			isActive=num;
       	}
       	else
       		if (num>0)
       	{
       	isActive=num;

       }
       }


