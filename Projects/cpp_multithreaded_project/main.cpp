#include "../include/CyberPC.h"
#include "../include/CyberWorm.h"
#include <boost/property_tree/ptree.hpp>
#include <boost/property_tree/xml_parser.hpp>
#include <boost/foreach.hpp>
#include "../include/CyberDNS.h"
#include "../include/CyberExpert.h"
using boost::property_tree::ptree;
using namespace std;
#include <iostream>
#include <map>

void runPcs(CyberDNS& currMap, std::vector<std::string> computers)
{
	for (unsigned i=0; i < computers.size(); i++) {
		CyberPC & cpc= currMap.GetCyberPC(computers[i]);
			cpc.Run(currMap);
		}
}

void runExperts(vector<CyberExpert*> workersList, std::vector<std::string> compList, CyberDNS& pcMap)
{

	int lastPc= 0;
	for (unsigned int k=0; k<workersList.size(); k=k+1)
	{
		CyberExpert *currExpert = workersList[k];
		if(currExpert->getIsActive()>0)
		{
			int fCounter= currExpert->getEfficiency();
			for(int i=0; i< fCounter; i=i+1)
			{
				if(lastPc < compList.size())
				{
					  cout<< "        "+ currExpert->getName() +" is examining " + compList[lastPc] << endl;
					  currExpert->Clean(pcMap.GetCyberPC(compList[lastPc]));
				  	  lastPc= lastPc + 1;
				}
			}
		}
		currExpert->setIsActive(currExpert->getIsActive()-1);

	}
}

int main(){
	int currTime=0;
	int timeLeft=0;
	vector<CyberExpert*> workers;
	std::vector<std::string> computersList;
	std::map<const std::string, CyberWorm & > cyber_worm;
	CyberDNS server= CyberDNS();

	ptree computers;
    read_xml("computers.xml",computers);

    ptree connections;
    read_xml("network.xml",connections);

    ptree events;
    read_xml("events.xml",events);


    BOOST_FOREACH(ptree::value_type &v,computers){
        string name=v.second.get<string>("name");
        string os=v.second.get<string>("os");
        CyberPC *pc = new CyberPC(os,name);
        cout << "adding to server: "<< name << endl;
        server.AddPC(*pc);
        delete pc;
    }

    BOOST_FOREACH(ptree::value_type &t, connections)
    {
    	string pointA= t.second.get<string>("pointA");
    	string pointB= t.second.get<string>("pointB");
    	CyberPC  &pA = server.GetCyberPC(pointA);
        CyberPC  &pB = server.GetCyberPC(pointB);
    	cout << "connecting "<< pointA << " to " << pointB  <<endl;
        pA.AddConnection(pointB);
        pB.AddConnection(pointA);
    }
    computersList= server.GetCyberPCList();


BOOST_FOREACH( ptree::value_type & k,events.get_child(""))
{
	if(k.first!= "termination")
	cout << "DAY  :   "<< currTime <<endl;

	if(k.first == "termination")
	{
		timeLeft= k.second.get<int>("time");
		break;
	}

	if(k.first == "hack")
		{
		string pcName= k.second.get<string>("computer");
		string wormName= k.second.get<string>("wormName");
		int wormDormancy = k.second.get<int>("wormDormancy");
		string wormOS = k.second.get<string>("wormOs");
		cout << "     Hack occured on "<< pcName <<endl;

		CyberWorm *currWorm = new CyberWorm(wormOS, wormName,wormDormancy);

		server.GetCyberPC(pcName).Infect(*currWorm);
		delete currWorm;
		}
	if(k.first == "clock-in")
		{
			string workerName= k.second.get<string>("name");
			int workTime = k.second.get<int>("workTime");
			int restTime = k.second.get<int>("restTime");
			int efficiency = k.second.get<int>("efficiency");
			CyberExpert *currWorker= new CyberExpert(workerName,workTime, restTime, efficiency);
			workers.push_back(currWorker);

	    	cout<<"Clock in: " << workerName << "  began working" << endl;
		}

		runExperts(workers, computersList, server);
		runPcs(server,computersList);
		currTime= currTime+1;
}

		while(currTime<= timeLeft)
		{
			cout << "DAY  :   "<< currTime <<endl;
			runExperts(workers, computersList, server);
			runPcs(server,computersList);
			currTime= currTime+1;
		}

		for(unsigned int i=0; i<workers.size(); i=i+1)
		{
			delete(workers[i]);
		}

		for(unsigned int i=0; i<computersList.size(); i=i+1)
		{
			delete(&server.GetCyberPC(computersList[i]));
		}
}






















