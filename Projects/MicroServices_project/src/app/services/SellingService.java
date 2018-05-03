package app.services;

import java.util.concurrent.CountDownLatch;
import java.util.logging.Logger;

import app.message.PurchaseOrderRequest;
import app.message.RestockRequest;
import app.message.TerminateBroadcast;
import app.message.TickBroadcast;
import app.runners.Receipt;
import app.runners.Store;
import app.runners.Store.BuyResult;

//important - if user wanted discount only, and there wasn't - he will get null from sellingService


import bgu.spl.mics.MicroService;




/**
 * 
 * SellingService is responsible to get {@link PurchaseOrderRequest} from the webclientServices and provide them their shoes they wanted from the store.
 * the sellingServices are communiting with the managerService in case of missing shoes, for restocking them.
 * <p>
 * 
 * @param name - name of SellingService
 * @param currTick -  current Tick
 * @param latchObj -  for correct timing
 */


public class SellingService extends MicroService {
	String name;
	private int currTick;
	private CountDownLatch latchObj;
	
	/**
	 * Main constructor
	 * @param name of the SellingService
	 * 
	 */
	
	
	
	
	public SellingService(String name, CountDownLatch countDownLatchObject) {
		super(name);
		latchObj= countDownLatchObject;
	}

	
	
	/**
	 * The SellingService is Subscribing to {@link PurchaseOrderRequest}. When he gets that request, he checks with the store for the availability of the shoe.
	 * The SellingService also gets from the buyer whether or not he wants the shoe in discounted price or not. in case the shoe that was demanded was discountedOnly, the selling
	 * service will complete the request with a receipt if there is a shoe in discount. If there isn't, the sellingService will complete the Buyer's request with null.
	 * in case the shoe was not found in store, the Store will send a new {@link RestockRequest} to the manager
	 * 
	 * 
	 */
	
	protected void initialize() {
		Logger logger = Logger.getLogger("abc");
		logger.warning (this.getName()+ " start.");
		subscribeBroadcast(TickBroadcast.class, b->{
			logger.config("SellingService 1 entered to TickBrodcast");
			currTick = b.getCurrTick();
			logger.config("SellingService 2 getting curr tick.");
		});
		
		subscribeRequest(PurchaseOrderRequest.class, r->{logger.warning ("2.2 SellingService: entered purchase request. shoe = "+r.getShoeType());
			BuyResult ans= Store.getInstance().take(r.getShoeType(), r.isOnlyDiscount());
			if (ans.equals(Store.BuyResult.NOT_IN_STOCK)){
				logger.severe("lol SellingService "+this.getName()+" buy result: not in stock. shoe = "+r.getShoeType());
				RestockRequest resReq = new RestockRequest(r.getShoeType(), this.getName(), currTick);
				logger.severe ("SellingService"+this.getName()+"  preparing restock requestshoe. shoe  = "+r.getShoeType());
				sendRequest(resReq,c->{
					logger.severe ("SellingService"+this.getName()+"  callback of RestockR = "+r.getShoeType());
					if (!c.booleanValue()){
						logger.warning ("SellingService 7 The result of the request is null - shoe = "+r.getShoeType());
						complete(r, null);
						logger.warning ("SellingService 8 complete request - shoe = "+r.getShoeType());
					}
					else
					{
						logger.warning ("SellingService 9 the request result is positive. shoe = "+r.getShoeType());
						Receipt receipt = new Receipt(this.getName(), r.getCustomer(), r.getShoeType(), r.isOnlyDiscount(), currTick, r.getRequestTick(), 1);
						logger.warning ("SellingService 10 create recipet . shoe = "+r.getShoeType());
						complete(r, receipt);
						logger.warning ("SellingService 11 complete request. shoe = "+r.getShoeType());
						Store.getInstance().file(receipt);
						logger.severe("reciept filled by sellservice " + this.getName() +  " for customr: " + receipt.getCustomer() + " for shoe type: " + receipt.getShoeType()); // kuper
						logger.warning ("SellingService 12 file the receipt . shoe = "+r.getShoeType());
					}
				});
			}
			else if (ans.equals(Store.BuyResult.NOT_ON_DISCOUNT)){ 
					logger.warning ("lol SellingService 13 - buyer "+r.getCustomer()+ " "+ r.getShoeType() +" requested shoes not in discount.");
					complete(r, null);
					logger.warning ("SellingService 14 complete request with null result. shoe  = "+r.getShoeType());
				  }
				else if (ans.equals(Store.BuyResult.REGULAR_PRICE)){ 
						logger.warning ("lol SellingService 15 -customer"+r.getCustomer()+ " "+ r.getShoeType() +" requested shoes bought in regular price.");
						Receipt receipt = new Receipt(this.getName(), r.getCustomer(), r.getShoeType(), r.isOnlyDiscount(), currTick, r.getRequestTick(), 1);
						logger.config ("SellingService 16 create receipt.");
						complete(r, receipt);
						logger.warning ("SellingService 17 complete the request with receipt. shoe  = "+r.getShoeType());
						Store.getInstance().file(receipt);
						logger.severe("reciept filled by sellservice " + this.getName() +  " for customr: " + receipt.getCustomer() + " for shoe type: " + receipt.getShoeType()); // kuper
						logger.config ("SellingService 18 store the receiptshoe  = "+r.getShoeType());
					}
				else if (ans.equals(Store.BuyResult.DISCOUNTED_PRICE)){ 
						logger.warning ("lol SellingService 19 - customer "+r.getCustomer()+" requested shoe in discount price. shoe  = "+r.getShoeType());
						logger.warning ("SellingService 20 - customer" + r.getCustomer()+ " "+ r.getShoeType() +" requested shoes bought in discounted price.");
						Receipt receipt = new Receipt(this.getName(), r.getCustomer(), r.getShoeType(), r.isOnlyDiscount(), currTick, r.getRequestTick(), 1);
						logger.info("SellingService 21 create receipt.");
						complete(r,receipt);	
						logger.warning ("SellingService 22: "+  this.getName() +"complete the request with receipt. shoe  = "+r.getShoeType());
						Store.getInstance().file(receipt);
						logger.severe("reciept filled by  sellservice" + this.getName() +  " for customr: " + receipt.getCustomer() + " for shoe type: " + receipt.getShoeType());
						logger.warning ("SellingService 23: store the receiptshoe  = "+r.getShoeType());	
					}	
		});
		
		subscribeBroadcast(TerminateBroadcast.class, callback->{logger.severe("SELLSERVICE terminated"); terminate();});    
		latchObj.countDown();
	}
}

