package app.jsonReader;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.concurrent.CountDownLatch;
import app.runners.DiscountSchedule;
import app.runners.PurchaseSchedule;
import app.runners.ShoeStorageInfo;
import app.services.ManagementService;
import app.services.SellingService;
import app.services.ShoeFactoryService;
import app.services.TimeService;
import app.services.WebsiteClientService;

public class dataAnalyzer {
	
private MainReaderObject mainObject;	
private CountDownLatch countDownLatchObject;

/**
 * Gets ther mainObject from the json file. this class holds methods to  organize all of the MainObject data, and give the information back 
 * in order to start the threads in the main class(ShoeStoreRunner)
 * @param mainObject - the main json object
 * the class contains methods that will eventually start the following threads(microServices):
 * TimeService
 * ManagementService
 * ShoeStoreFactories
 * SellingServices
 * WebSiteClient
 * * and it will also have a method to organize the ShoeStorageInfo
 */

public dataAnalyzer (MainReaderObject mainObject){
	this.mainObject=mainObject;
	int numberOfThreads =mainObject.services.customers.length + mainObject.services.factories + mainObject.services.sellers + 1; 
	this.countDownLatchObject = new CountDownLatch(numberOfThreads);
}


public TimeService getTimeService() {

	TimeService timeserv = new TimeService(mainObject.services.time.speed, mainObject.services.time.duration, countDownLatchObject);
	return timeserv;

}




public ShoeStorageInfo[] getShoeStorageInfo (){
	ArrayList<ShoeStorageInfo> storageInfo = new ArrayList<ShoeStorageInfo>();
	for (int i=0; i<mainObject.initialStorage.length ; i++){
		ShoeStorageInfo shoeStorageInfo = new ShoeStorageInfo(mainObject.initialStorage[i].shoeType, mainObject.initialStorage[i].amount, 0 );
		storageInfo.add(shoeStorageInfo);
	}
	ShoeStorageInfo[] storeArray = new ShoeStorageInfo[storageInfo.size()]; 
	for (int i=0; i<storageInfo.size(); i++){
		storeArray[i]= storageInfo.get(i);
	}
	return storeArray;
}





public ManagementService getManagementService(){
	discountSchedule[] discountScheduleArray = mainObject.services.manager.discountSchedule;
	List<DiscountSchedule> discountScheduleList = new ArrayList<DiscountSchedule>();
	for (int i=0; i<discountScheduleArray.length; i++){
		discountScheduleList.add(new DiscountSchedule(discountScheduleArray[i].shoeType, discountScheduleArray[i].tick, discountScheduleArray[i].amount));
	}

	ManagementService managment = new ManagementService(discountScheduleList, countDownLatchObject);
	return managment;
}
	

public ArrayList<ShoeFactoryService> getShoeStoreFactories (){
	ArrayList<ShoeFactoryService> shoeFactoryServiceList = new ArrayList<ShoeFactoryService>();
	int factories = mainObject.services.factories;
	for (int i=0; i<factories; i++){
		ShoeFactoryService shoeFactoryService = new ShoeFactoryService ("factory number: " + i ,countDownLatchObject);
		shoeFactoryServiceList.add(shoeFactoryService);
	}
	return shoeFactoryServiceList;
}


public ArrayList<SellingService> getSellingServices(){
	ArrayList<SellingService> sellingServiceList = new ArrayList<SellingService>();
	int sellers = mainObject.services.sellers;
	for(int i=0; i<sellers; i++){
		SellingService sellingService = new SellingService("seller: " + i , countDownLatchObject);
		sellingServiceList.add(sellingService);
	}
	return sellingServiceList;
}


	
public ArrayList<WebsiteClientService> getListWebSiteClient (){
	ArrayList<WebsiteClientService> listWebsiteClientService = new ArrayList<WebsiteClientService>();
	
	for (int i=0; i<mainObject.services.customers.length; i++){
		Set<String> setWishList = new HashSet<String>();
		for (int k=0; k<mainObject.services.customers[i].wishList.length; k++){
			setWishList.add(mainObject.services.customers[i].wishList[k]);
		}
		ArrayList<PurchaseSchedule> purchaseScheduleList = new ArrayList<PurchaseSchedule>();
		for (int j=0 ; j<mainObject.services.customers[i].purchaseSchedule.length; j++){
			String shoeType = mainObject.services.customers[i].purchaseSchedule[j].shoeType;
			int tick = mainObject.services.customers[i].purchaseSchedule[j].tick;
			PurchaseSchedule purchaseScheduleToAdd = new PurchaseSchedule(shoeType, tick);
			purchaseScheduleList.add(purchaseScheduleToAdd);					
		}
		WebsiteClientService newWebsiteClientService = new WebsiteClientService(mainObject.services.customers[i].name, purchaseScheduleList, setWishList, countDownLatchObject);
		listWebsiteClientService.add(newWebsiteClientService);
	
	}

	return listWebsiteClientService;
}

}