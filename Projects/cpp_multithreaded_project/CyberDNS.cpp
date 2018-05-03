#include "../include/CyberDNS.h"
using namespace std;

    CyberDNS::CyberDNS()
    {
     std::map<const std::string, CyberPC & > cyber_DNS_;
    }

    std::map<const std::string, CyberPC &> CyberDNS::getMap()
		{
    	return cyber_DNS_;
		}

	void CyberDNS::AddPC(CyberPC & cyber_pc_)
	{
	std::map<const std::string, CyberPC&>::iterator it;
	CyberPC *cpc= new CyberPC(cyber_pc_);
	const string cpcName= cpc->getName();
	{
	  cyber_DNS_.insert(std::pair<const std::string, CyberPC & >(cpcName,*cpc));
      cout << cyber_pc_.getName() << " is now connected to server"<< endl;
      cpc=NULL;
	  }
	}


CyberPC &  CyberDNS::GetCyberPC(const std::string & cyber_pc_name) const
	{
	    return cyber_DNS_.at(cyber_pc_name);
	  }

	std::vector<std::string> CyberDNS::GetCyberPCList()
	{
	std::vector<std::string> pcListNames;
	map<const string, CyberPC &>::reverse_iterator it;
	for (it = cyber_DNS_.rbegin(); it!= cyber_DNS_.rend(); ++it)
		pcListNames.push_back(it->first) ;

	return pcListNames;
}


