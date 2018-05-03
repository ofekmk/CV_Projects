#include "../include/CyberPC.h"
using namespace std;


  CyberPC::CyberPC(std::string cyber_pc_os, std::string cyber_pc_name):cyber_pc_os_(cyber_pc_os), cyber_pc_name_(cyber_pc_name)
  {
  std::vector<std::string> cyber_pc_connections_;
  cyber_pc_time_to_infect_ = -1;
  }

  CyberPC::~CyberPC()
  {
	  if(cyber_worm_!=NULL)
	  {
		  delete cyber_worm_;
		  cyber_worm_ = NULL;
	  }
	  cyber_pc_connections_.clear();
  }

    const std::string CyberPC::getName(){
      return cyber_pc_name_;
    }

    const std::string CyberPC::getOs()
    {
    	return cyber_pc_os_;
    }

	int CyberPC::getTimeToInfect(){
		return cyber_pc_time_to_infect_;
	}

	std::vector<std::string> CyberPC::getConnections()
		{
			return cyber_pc_connections_;
		}

	CyberWorm* CyberPC::getWorm()
	{
		if(cyber_worm_ != NULL)
		{return cyber_worm_;}
		else
		{return NULL;}
	}

	void CyberPC::AddConnection(std::string  second_pc)   {
    cout << "        " <<  cyber_pc_name_ <<" is now connected to " <<  second_pc <<endl;
	cyber_pc_connections_.push_back(second_pc);
	}


	void CyberPC::Infect(CyberWorm & worm){

		if(this->getName()!= worm.getName()){
          if (cyber_pc_os_== worm.getOs()){
        	  if(cyber_worm_ !=NULL)
        		  delete cyber_worm_;
        	  cyber_worm_ = new CyberWorm(worm);
        	  cyber_pc_time_to_infect_= worm.getDormancyTime();
  			cout << "                "<<this->getName() << " infected by " << worm.getName() <<endl;
          }
  		else
  		{
           cout << "        Worm " << worm.getName() << " is incompitiable with " << this->getName() << endl;
  		}
  		}

       }


	void CyberPC::Run(const CyberDNS & server){	// Activate PC and infect others if worm is active
	    	if(cyber_pc_time_to_infect_!=-1)
	    	{
	    		if(cyber_pc_time_to_infect_ ==0)
	    			{
	    				cout << "        "+this->getName() <<" infecting..." << endl;
	    				for (unsigned i=0; i < cyber_pc_connections_.size(); i++) {
	    					CyberPC & cpc= server.GetCyberPC(cyber_pc_connections_[i]);
	    						cpc.Infect(*cyber_worm_);
	    					}
	    				return;
	    			}
	    		cyber_pc_time_to_infect_ = cyber_pc_time_to_infect_ -1;
	    			if(this->getWorm()!=NULL)
	    			{
	    			cout <<"                "<< this->getName() << ": Worm " << this->getWorm()->getName()<< " is dormant" <<endl;
	    			}
	    	}
	}

	void CyberPC::Disinfect(){
		if(cyber_pc_time_to_infect_ > -1)
		{
			cyber_pc_time_to_infect_ = -1;
			if(cyber_worm_ !=NULL)
			cout<< "                "+this->getWorm()->getName() <<" successfully removed from " << this->getName() << endl;
			delete cyber_worm_;
			cyber_worm_ = NULL;
		}
	}

