package app.services;
import java.util.List;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.CountDownLatch;
import java.util.logging.Logger;

import app.message.NewDiscountBroadcast;
import app.message.PurchaseOrderRequest;
import app.message.TerminateBroadcast;
import app.message.TickBroadcast;
import app.runners.PurchaseSchedule;
import bgu.spl.mics.MicroService;



/**
 * 
 * The WebsiteClientService is a client that wants to buy a new shoe. he will subscribe to {@link NewDiscountBroadcast}, to see if 
 * his desired shoe is in that message(the buyer's shoes wishlist are taken from from {@value purchaseSchedualeMap} whether or not there is a shoe that he wants to buy in the current tick.
 * if there is a shoe in the specific tick, he'll send a {@link PurchaseOrderRequest} to the sellingService.
 *
 *<p>
 *@param latchObj - used for correct timing with all of the other MicroServices
 *@param purchaseSchedualeMap -  holds all of the buyers \\purchaseScheduale objects
 *@param wishList - holds the buyer's shoes that he wants to purchase in discount
 *@param currTick - current tick 
 *@param flag - used for synchronization
 */

public class WebsiteClientService extends MicroService {
	private CountDownLatch latchObj;
	private ConcurrentHashMap<Integer, CopyOnWriteArrayList<PurchaseSchedule>> purchaseSchedualeMap;
	Set<String> wishList;
	int currTick;
	private Object flag;
	
	
	/*
	 * WebsiteClientService constructor 
	 */
	
	public WebsiteClientService(String name, List<PurchaseSchedule> purchaseScheduleList, Set<String> setWishList,CountDownLatch countDownLatchObject) {
		super(name);
		latchObj = countDownLatchObject;
		this.wishList = setWishList;
		currTick=-1;
		purchaseSchedualeMap= new ConcurrentHashMap<Integer, CopyOnWriteArrayList<PurchaseSchedule>>();
		flag = new Object();
	
		for(int i=0;i<purchaseScheduleList.size(); i=i+1){
			Integer currTick = new Integer(purchaseScheduleList.get(i).getTick());
			if(purchaseSchedualeMap.containsKey(currTick)){ // if the hash map conatains a list with the same tick.
				purchaseSchedualeMap.get(currTick).add(purchaseScheduleList.get(i));
			}
			else{
				CopyOnWriteArrayList<PurchaseSchedule> currList= new CopyOnWriteArrayList<PurchaseSchedule>();
				currList.add(purchaseScheduleList.get(i));
				purchaseSchedualeMap.put(currTick, currList);
				}
			}
		}
	

	public Set<String> getWishList() {
		return wishList;
	}
	public void setWishList(Set<String> wishList) {
		this.wishList = wishList;
	}
	
	
	/**
	 * Part A - the buyer  subscribes to {@link TickBroadcastt} , and he'll check in his {@value purchaseSchedualeMap} whether or not there is a shoe that he wants to buy in the current tick.
	 * if there is a shoe in the specific tick, he'll send a {@link PurchaseOrderRequest} to the sellingService.
	 * 
	 * 
	 * Part B- the buyer subscribes to{@link NewDiscountBroadcast}, to see if 
	 * his desired shoe is in that message(the buyer's shoes wishlist are taken from from {@value purchaseSchedualeMap}. if a tick matches his shoe, he'll send a {@link PurchaseOrderRequest} to the sellingService. the buyer 
	 * will get a receipt when the selling service is done with his request.
	 * 

	 *
	 */
	
	@Override
	protected void initialize() {
		if (this.wishList.isEmpty()&&this.purchaseSchedualeMap.isEmpty()){this.terminate();}
			Logger logger = Logger.getLogger("abc");
			logger.warning (this.getName()+"wcService 1:"+this.getName()+" start.");
			subscribeBroadcast(TickBroadcast.class, b->{
				logger.config (this.getName()+"wcService 2: Subscribed to tick number: " + b.getCurrTick());
				currTick=b.getCurrTick();
				if(!(this.purchaseSchedualeMap.isEmpty()))
				synchronized(flag){
				if(purchaseSchedualeMap.containsKey(currTick)){
					logger.config (this.getName()+"wcService 4: 	if(purchaseSchedualeMap.containsKey(currTick)");
					CopyOnWriteArrayList<PurchaseSchedule> currList = purchaseSchedualeMap.get(currTick);
					for(int i=0;i<currList.size();i=i+1){
						PurchaseSchedule currPurRequest = currList.get(i);
						logger.config (this.getName()+"wcService 5: PurchaseSchedule currRequest = currList.get(i);");
						PurchaseOrderRequest request = new PurchaseOrderRequest(this.getName(),currPurRequest.getShoeType() ,false, currTick); 
						if (this.wishList.contains(currPurRequest.getShoeType())){
							logger.config (this.getName()+"wcService 6: this.wishList.contains(currRequest.getShoeType()");
							this.wishList.remove(currPurRequest.getShoeType());
							logger.config (this.getName()+"wcService 7: wishList.remove(currRequest.getShoeType()");
						}
						logger.severe(this.getName()+"going to send sendR from purchacelist. shoe = "+request.getShoeType());
						sendRequest(request,rec->{	
							if(rec!=null) logger.severe (this.getName()+"wcService 8- ******** bought(not discount) *********)"+request.getShoeType());
							logger.warning (this.getName()+"wcService 8- sendRequest taking care");
						});
						logger.config (this.getName()+"wcService 9");	
					}
				logger.config (this.getName()+"wcService 10: purchaseSchedualeMap.remove(currTick)");	
				purchaseSchedualeMap.remove(currTick);
			   }
				}
			});
    
		subscribeBroadcast(NewDiscountBroadcast.class, discountB->     
		{
			String currShoe = discountB.getShoeType();
			logger.warning (this.getName()+" wcService 13: Subscribed to new discount brodcast.");
			if (this.wishList.contains(currShoe)){
				logger.info (this.getName()+"wcService 17: " + currShoe + "is available in discount.");
				PurchaseOrderRequest request = new PurchaseOrderRequest(this.getName(),currShoe,true, discountB.getCurrTick());
				logger.severe (this.getName()+"going to send request(from wish list) for shoe = "+currShoe);
				sendRequest(request, rec->{ 
					if(rec!=null)
						logger.severe (this.getName()+"wcService 19: ******* bought in Discount****** SendR callback! (wishlist) shoe = "+currShoe);
					
				wishList.remove(currShoe);
				});
			}
		});
		

		
		logger.config (this.getName()+"wcService 31");
		subscribeBroadcast(TerminateBroadcast.class, callback->{logger.warning("WEBCLIENT terminated"); terminate();});    
		latchObj.countDown();
		}}


		



