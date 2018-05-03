package app.runners;



import java.util.LinkedList;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.logging.Logger;

import bgu.spl.mics.Callback;
import bgu.spl.mics.MicroService;
import bgu.spl.mics.Request;





/**
 * 
 * The store stores the shoes in \\storeStorage as \\Shoeinfo objects.
 * The Selling service will ask the store for each \\PurchaseRequest made by the user. The store
 * will give a proper answer for the sellingService demand.
 * The store also respinsible to hold the list of \\receipts.(filing them, printing them and etc).
 * <p>
 * @param storeStorage - A map that holds all of the \\ShoeStorageInfo
 * @param storeReciepts - a list that holds all of the \\Receipt
 */
public class Store {
	private ConcurrentHashMap<String,ShoeStorageInfo> storeStorage ;
	private  LinkedList<Receipt> storeReciepts;
	
	
	public enum BuyResult{
		NOT_IN_STOCK,NOT_ON_DISCOUNT, REGULAR_PRICE, DISCOUNTED_PRICE;	
	}
	

	
	/**
	 * 
	 * The store is a thread-safe singleton
	 *
	 */
	private static class SingletonHolder {
		public static Store instance = new Store();
	}


	private Store(){
		storeStorage= new ConcurrentHashMap<String, ShoeStorageInfo>();
		storeReciepts= new LinkedList<Receipt>();
	}
	
	
	public static Store getInstance() {
		return SingletonHolder.instance;
	}

	
	
	public LinkedList<Receipt> getStoreReciepts() {
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 1");
		return storeReciepts;
	}

	public void setStoreReciepts(LinkedList<Receipt> storeReciepts) {
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 2");
		this.storeReciepts = storeReciepts;
	}

	public  ConcurrentHashMap<String, ShoeStorageInfo> getStoreStorage() {
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 3");
		return storeStorage;
	}

	public void setStoreStorage(
			
			ConcurrentHashMap<String, ShoeStorageInfo> storeStorage) {
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 4");
		this.storeStorage = storeStorage;
	}

	/**
	 * Loading all the info from the .json file to the store
	 * @param storage - the array of all the \\ShoeStorageInfo
	 */
	
	public void load ( ShoeStorageInfo [ ] storage ){
	
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 5");
			for(int i=0; i< storage.length; i=i+1)
			{
				this.storeStorage.put(storage[i].getShoeType(), storage[i]);
			}	
		}
	
	/**
	 * The sellingService uses this method in order to take a shoe from the store. The store will then give back a {@link BuyResult} of the proper answer.
	 * <p>
	 * @param shoeType - the requested shoe 
	 * @param onlyDiscount - Boolean parameter , to know whether or not the demanded shoe is demanded in discount only or not.
	 * @return - {@link BuyResult} of the proper answer.
	 */

	public BuyResult take( String shoeType , boolean onlyDiscount )
	{
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 6"); 
		if(shoeType!=null && storeStorage.containsKey(shoeType)){
		ShoeStorageInfo currShoe= this.storeStorage.get(shoeType);
		
		
		
		synchronized (currShoe){
			logger.warning("*******amount ___" + currShoe.getAmountOnStorage() + "    shoe type: " + currShoe.getShoeType());

			logger.info ("store 7");		
			
		if((onlyDiscount )){
			logger.severe ("store 9-NOT_ON_DISCOUNT");
			if (currShoe.getDiscountedAmount()==0)
			return BuyResult.NOT_ON_DISCOUNT;
			else{
				logger.info ("store 10");
				currShoe.setDiscountedAmount(currShoe.getDiscountedAmount()-1);
				currShoe.setAmountOnStorage(currShoe.getAmountOnStorage()-1);
				if (currShoe.getAmountOnStorage()==0){
					logger.severe ("store 11-DISCOUNTED_PRICE");
					this.storeStorage.remove(currShoe.getShoeType());
				}
				return BuyResult.DISCOUNTED_PRICE;
			}
		}
		
		else if(currShoe.getAmountOnStorage()>0)
		{
			if (currShoe.getDiscountedAmount()>0){
				currShoe.setDiscountedAmount((currShoe.getDiscountedAmount()-1));
				return BuyResult.DISCOUNTED_PRICE;
			}
			else{
				
			logger.severe ("store 12-REGULAR_PRICE");
			currShoe.setAmountOnStorage(currShoe.getAmountOnStorage()-1);
			if (currShoe.getAmountOnStorage()<=0)//yarin
				this.storeStorage.remove(currShoe.getShoeType());
			return BuyResult.REGULAR_PRICE;
			}
		}
		}
		}
		return BuyResult.NOT_IN_STOCK;
	}
	

	
	/**
	 * The method adds a new shoe to storage( as a \\ShoeStorageInfo object). this method will be used by the manager, in case that there are supplements for any kind of shoe
	 * that he got back from the factory.
	 * @param shoeType -  the type of the shoe that will be added
	 * @param amount - amount of shoe to be added
	 */
	
	public void add (String shoeType , int amount ){
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 13");
		if(storeStorage.containsKey(shoeType)){
			logger.info ("store 14");
			this.storeStorage.get(shoeType).setAmountOnStorage(amount+ this.storeStorage.get(shoeType).getAmountOnStorage());
		}
		else{
			logger.info ("store 15");
			storeStorage.put(shoeType, new ShoeStorageInfo(shoeType, amount, 0));
			logger.info ("store 16");
		}
	}
	
	/**
	 * Adding discount to specific shoe(done by manager)
	 * @param shoeType - shoeType
	 * @param amount - amount to add
	 */
	public void addDiscount ( String shoeType , int amount ) {	
		
		
	
		if(storeStorage.containsKey(shoeType)){
			Logger logger = Logger.getLogger("abc");
			logger.info ("store 17");
			if(this.storeStorage.get(shoeType).getAmountOnStorage()< this.storeStorage.get(shoeType).getDiscountedAmount()+amount)
			this.storeStorage.get(shoeType).setDiscountedAmount(this.storeStorage.get(shoeType).getAmountOnStorage());
			else
				this.storeStorage.get(shoeType).setDiscountedAmount(amount + this.storeStorage.get(shoeType).getDiscountedAmount());
		}
	}
	
	/**
	 * Filing the \\receipt to the store(\\storeReciepts)
	 * @param receipt -  the given receipt to file
	 */
	
	public void file (Receipt receipt){
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 19");
		if(receipt!= null){
			storeReciepts.addLast(receipt);
			logger.info ("store 20");
		}
	}
	
	/**
	 * printing all of the \\storeReciepts receipts
	 */
	
	public void print ( ){
		Logger logger = Logger.getLogger("abc");
		logger.info ("store 21");
		for(String key: storeStorage.keySet())
		{
			
			storeStorage.get(key).toString(); 
		}		
		int count=0;
		for (Receipt queueIT : storeReciepts) 
		{
			count=count+1;
			System.out.println("**************Reciept number: "+ count +"**************");
			System.out.println(queueIT.toString());
		}
	}
}


