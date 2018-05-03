package app.runners;


import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.ArrayList;
import java.util.logging.Level;
import java.util.logging.Logger;
import com.google.gson.Gson;
import app.jsonReader.MainReaderObject;
import app.jsonReader.dataAnalyzer;
import app.services.ManagementService;
import app.services.SellingService;
import app.services.ShoeFactoryService;
import app.services.TimeService;
import app.services.WebsiteClientService;



/**
 * 
 * The ShoeStoreRunner is responsible to read the input data from a .json file.
 * it will collect all the data 
 * <p>
 * @param gson - the Gson library containg methods to read json files
 * @param reader - uses for json input method
 *@param logger - for printing data to screen by given priority
 *@param store - the store singleton
 *@param mainObj - the main Object that holds all of the json objects(will hold initialStorage and services objects)
 *@param da - an object with methods that organize and give back the .json data in order to start all of the microservices(threads)
 *@param arraylistShoeStoreFactories -  contains the data(and amount of) for starting the ShoeFactories services
 *@param arraylistSellingServices - contains the data(and amount of) for starting the SellingServices services
 *@param arraylistWebsiteClientService -contains the data(and amount of) for starting the WebsiteClientService services 
 *@param ShoeStorageInfoArray - contains the ShoeStorage information
 *@param timeService - contains the data for starting the timeService service
 *@param manager - contains the data for starting the ManagmentService service
 *
 */

public class ShoeStoreRunner {

	public static void main(String [] args) {
		Gson gson = new Gson();
		BufferedReader reader = null;
		Logger logger = Logger.getLogger("abc");
        logger.setLevel(Level.SEVERE);
		try{
			reader= new BufferedReader(new FileReader(args[0]));
		}
		catch(FileNotFoundException e){
			System.out.println("file not found");
			e.printStackTrace();
		}
		
		Store store = Store.getInstance();
		MainReaderObject mainObj = gson.fromJson(reader, MainReaderObject.class);
		dataAnalyzer da = new dataAnalyzer(mainObj);
		ArrayList<ShoeFactoryService> arraylistFactories = da.getShoeStoreFactories();
		ArrayList<SellingService> arraylistSellingServices = da.getSellingServices();
		ArrayList<WebsiteClientService> arraylistWebsiteClientService = da.getListWebSiteClient();
		ShoeStorageInfo[] ShoeStorageInfoArray = da.getShoeStorageInfo();
		TimeService timeService = da.getTimeService();
		ManagementService manager = da.getManagementService();
		store.load(ShoeStorageInfoArray);
		
		for (int i = 0; i<ShoeStorageInfoArray.length;i++)
			System.out.println(ShoeStorageInfoArray[i].toString());
		
		System.out.println();
		System.out.println("Number of Factories= "+arraylistFactories.size());
		System.out.println();
		System.out.println("Number of sellers= "+arraylistSellingServices.size());
		System.out.println();
		System.out.println("Number of Buyers = "+arraylistWebsiteClientService.size());
		System.out.println();


		
		for (int i =0; i<arraylistWebsiteClientService.size(); i++){
			Thread clientThread = new Thread(arraylistWebsiteClientService.get(i));
			clientThread.start();
		}	
		
			Thread timerThread = new Thread(timeService);
			timerThread.start();
			
			for (int i =0; i<arraylistFactories.size(); i++){
				Thread factoryThread = new Thread(arraylistFactories.get(i));
				factoryThread.start();
			}
		
			Thread managerThread = new Thread(manager);
			managerThread.start();
		
		
			
			for (int i =0; i<arraylistSellingServices.size(); i++){
				Thread sellerThread = new Thread(arraylistSellingServices.get(i));
				sellerThread.start();
			}
			
		
			
			
	
		
		
		
		
		
		
		
		
		
	}
}



