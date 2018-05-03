package app.services;

import java.util.List;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.CountDownLatch;
import java.util.logging.Logger;

import app.message.ManufacturingOrderRequest;
import app.message.NewDiscountBroadcast;
import app.message.RestockRequest;
import app.message.TerminateBroadcast;
import app.message.TickBroadcast;
import app.runners.DiscountSchedule;
import app.runners.Store;
import bgu.spl.mics.MicroService;




/**
 * The  ManagmentService is responsible to check every given tick whether or not
 * to send a {@link NewDiscountBroadcast}. 
 * he is also responsible to send {@link ManufacturingOderRequest} in the case 
 * there are missing shoes in the store.
 * 
 */

public class ManagementService extends MicroService{

	private int currTick;
	private ConcurrentHashMap<Integer, CopyOnWriteArrayList<DiscountSchedule>> discountSchedualeMap;
	private ConcurrentHashMap<String,CopyOnWriteArrayList<ManufacturingOrderRequest>> shoeTypeAndManufacturingRequestsarrayList;
	private ConcurrentHashMap<ManufacturingOrderRequest,Integer> ManufacturingRequestAndNumberOfShoesAvailable;
	private ConcurrentHashMap<ManufacturingOrderRequest,CopyOnWriteArrayList<RestockRequest>> ManufacturingRequestAndRestockRequest;
	private CountDownLatch latchObject;
	
	
	
	/**
	 * The ManagementService constructor gets a {@link NewDiscountBrodiscountSchedualeadcast} and a  {@link CountDownLatch}
	 * <P>
	 * @param shoeTypeAndManufacturingRequestsarrayList - When the sellingService senders a restock request to the manager, the manager searches for this shoe in this map. if it's
	 *not there, it will create a new key(shoeType) and it's array of ManufactureRequests.
	 * @param ManufacturingRequestAndNumberOfShoesAvailable holds the manufacture order request and the number of shoes left for new requesters of this kind of shoe. if the number 
	 * of shoes left is 0, the next RestockRequest of this specific shoe will force the manager to create a new ManufactureOrder.
	 * @param ManufacturingRequestAndRestockRequest is the map that responsible to return completes for the RestockRequests that requested a shoe from the specific ManufactureOrderRequest
	 * @param discountSchedualeMap -  hold the discountSchedual values given from the user(json)
	 */
	
	public ManagementService(List<DiscountSchedule> discountScheduale, CountDownLatch latch){
		super("manager");	
		this.latchObject = latch; 
	    currTick=-1;
	    shoeTypeAndManufacturingRequestsarrayList =  new ConcurrentHashMap<String,CopyOnWriteArrayList<ManufacturingOrderRequest>>();
	    ManufacturingRequestAndNumberOfShoesAvailable=  new ConcurrentHashMap<ManufacturingOrderRequest,Integer>();
	    ManufacturingRequestAndRestockRequest =new ConcurrentHashMap<ManufacturingOrderRequest,CopyOnWriteArrayList<RestockRequest>>();
	    discountSchedualeMap= new ConcurrentHashMap<Integer,CopyOnWriteArrayList<DiscountSchedule>>();
	    
		for (DiscountSchedule it:discountScheduale){
					Integer currTick = new Integer(it.getTick());
					if(discountSchedualeMap.containsKey(currTick)){ 
						discountSchedualeMap.get(currTick).add(it);
					}
					else{
						CopyOnWriteArrayList<DiscountSchedule> currList= new CopyOnWriteArrayList<DiscountSchedule>();
						currList.add(it);
						discountSchedualeMap.put(currTick, currList);
					}
				}
			}
	


	
	
	/**
	 * 
	 *  ManagerSenNewManufactureOrderRequest function responsible of sending \\ManufacturingOrderrequest
	 * 
	 * @param manufReqFunctio - sending new ManufactureOrderRequest with this parameter.
	 */
	
	private void ManagerSenNewManufactureOrderRequest(ManufacturingOrderRequest manufReqFunction){
		Logger logger = Logger.getLogger("abc");
		sendRequest(manufReqFunction, onComplete->{
				logger.severe ("ManagementService - **************************oncomplete of manufRequest********************** ="+manufReqFunction.getShoeType()+" amount= "+manufReqFunction.getAmount());

					if (onComplete==null){ 
						logger.warning ("32-managservice - onComplete - no factory cares for manager's sent request!! shoe = "+ manufReqFunction.getShoeType());
						CopyOnWriteArrayList<RestockRequest> sendingCompletesList =this.ManufacturingRequestAndRestockRequest.get(manufReqFunction);
						for (int i=0;i<sendingCompletesList.size();i++){
							complete(sendingCompletesList.get(i), new Boolean(false));
							logger.warning ("33-managservice - completeing with null! shoe = "+manufReqFunction.getShoeType());
						}	
					}
					else {	
						if (this.ManufacturingRequestAndNumberOfShoesAvailable.get(manufReqFunction)>0){
							logger.info ("21-managservice- supplement shoes will be set in store, shoe="+manufReqFunction.getShoeType());
							Store.getInstance().add(manufReqFunction.getShoeType(), this.ManufacturingRequestAndNumberOfShoesAvailable.get(manufReqFunction));
					       logger.severe("*********MANAGMENT PUT SUPPLEMENTS TO STORE********* shoe= " +manufReqFunction.getShoeType()+"amount=  "+this.ManufacturingRequestAndNumberOfShoesAvailable.get(manufReqFunction));
							logger.info ("22-managservice - supplement shoes put in store, shoe = "+manufReqFunction.getShoeType()+"amount(supplement)= "+this.ManufacturingRequestAndNumberOfShoesAvailable.get(manufReqFunction));
						}
						this.ManufacturingRequestAndNumberOfShoesAvailable.replace(manufReqFunction, 0);
						Store.getInstance().file(onComplete);
						logger.severe ("34-managservice - factory cared about the manager, and created for shoe = "+manufReqFunction.getShoeType()+"amount= "+manufReqFunction.getAmount());
                        CopyOnWriteArrayList<RestockRequest> sendingCompletesList =this.ManufacturingRequestAndRestockRequest.get(manufReqFunction);
						for (int i=0;i<sendingCompletesList.size();i++){
							complete(sendingCompletesList.get(i), new Boolean(true));
							logger.info ("35-managservice - setting reciept in store reciept list.");
							logger.warning ("36-managservice - completed manufRequest, for shoe = "+manufReqFunction.getShoeType());
						}
					
					}

			});	
		
		
		
		
		
		
	}	/**
	 * Part A of the initialize is to subscribe to \\TickBroadcast and check in its \\discountSchedulemap if the current tick(key) contains any \\discountSchedule.
	 * for any \\discountSchedule that is registered to the current tick, the manager will send a broadcast of the specific shoe, and it's amount, taken from
	 * the \\discountSchedule
	 * 
	 * 
	 * Part B is consists of 4 main path-ways. First of all, the manager subscribes to \\RestockRequest. 
	 * Part B.1 - if the shoe of the given \\RestockRequest is not
	 * found in the @param shoeTypeAndManufacturingRequestsarrayList, he will create a new \\manufactureOrderRequest and put the new shoe and an array  consisting
	 * this \\manufactureOrderRequest , and put them in  @param shoeTypeAndManufacturingRequestsarrayList.
	 * 
	 * Part B.2 - if the shoe was found in @param shoeTypeAndManufacturingRequestsarrayList,
	 * the manager will peek to the last m\\manufactureorderrequest of this shoeType, and then he will look forward into @param ManufacturingRequestAndNumberOfShoesAvailable
	 * to see if the number of shoes left is >0. in case there are no shoes left, he will continue to part B.3
	 * 
	 * Part B.3 - The manager creates a new \\manufactureorderrequest, and then he'll repeat his steps from B.1
	 * 
	 * Part B.4 - If number of shoes available in @param shoeTypeAndManufacturingRequestsarrayList are>0, then the manager will just decrease the number by 1, and will keep
	 * the \\RestockRequest in   @param ManufacturingRequestAndRestockRequest
	 * 
	 * ** Note - for each RestockRequest, the manager keeps it in the correlated \\manufacturorderRequest, in  @param ManufacturingRequestAndRestockRequest
	 */
	
	
	
	
	
	

	@Override
	protected void initialize() {
		Logger logger = Logger.getLogger("abc");
		logger.warning ("1-managservice-initialize");
		subscribeBroadcast(TickBroadcast.class, t->{
			logger.config ("2-managservice-recievcd ticket broadcast="+t.getCurrTick());
			this.currTick=t.getCurrTick();
			logger.config ("3-managservice");
			currTick= new Integer(t.getCurrTick());
				if(discountSchedualeMap.containsKey(currTick)){
					logger.info ("4-managservice-going to send a discountBroadcast");
					CopyOnWriteArrayList<DiscountSchedule> currList = discountSchedualeMap.get(currTick);
					for(int i=0;i<currList.size();i=i+1){
						logger.config ("5-managservice");
						Store.getInstance().addDiscount(currList.get(i).getShoeType(), currList.get(i).getAmount());
						NewDiscountBroadcast currDiscount = new NewDiscountBroadcast(currList.get(i).getShoeType(),currList.get(i).getAmount(), currList.get(i).getTick());
						sendBroadcast(currDiscount);
						logger.severe ("6-************************managservice-Discount broadcast sent:******************************* shoe="+currDiscount.getShoeType()+"amount= "+currDiscount.getAmount());

							
					}
				}
					discountSchedualeMap.remove(currTick); 
					logger.info ("7managservice- discountSchedual was removed from list");
						
		});
		
		subscribeRequest(RestockRequest.class, r->{
			logger.warning ("8-ManagementService - recieved RestockRequest, shoetype="+r.getShoeType());
			String shoeType = r.getShoeType();
			logger.config ("10-managservice");
			

			if (this.shoeTypeAndManufacturingRequestsarrayList.get(shoeType)==null){
				logger.info ("11-managservice- shoe "+shoeType+"~ does not appear in factory~, will create a new demand");
				CopyOnWriteArrayList<ManufacturingOrderRequest> arrayList = new CopyOnWriteArrayList<ManufacturingOrderRequest>();
				logger.config ("12-managservice");
				ManufacturingOrderRequest manufactureOrderR = new ManufacturingOrderRequest(shoeType, (this.currTick%5 +1) ,this.currTick);
				logger.config ("13-managservice");
				arrayList.add(manufactureOrderR);
				logger.info ("14-managservice - put manufactureOrderR in the 3 hashmaps");	
				shoeTypeAndManufacturingRequestsarrayList.put(shoeType,arrayList);
				logger.config ("15-managservice");
				this.ManufacturingRequestAndNumberOfShoesAvailable.put(manufactureOrderR, this.currTick%5);
				logger.config ("16-managservice");
				CopyOnWriteArrayList<RestockRequest> list = new CopyOnWriteArrayList<RestockRequest>();
				logger.config ("17-managservice");
				list.add(r);
				logger.config ("18-managservice");
				this.ManufacturingRequestAndRestockRequest.put(manufactureOrderR, list);
				logger.config ("19-managservice");
				logger.info ("ManagementService - preparing for new Request of shoe to manufactory!!");	
				//here	
				ManagerSenNewManufactureOrderRequest(manufactureOrderR);
			}
		
			else{
				logger.info ("38-managservice - Shoe is being created in factory. we need to know whether we need to send a new ManufR or not. shoe= "+shoeType);
				CopyOnWriteArrayList<ManufacturingOrderRequest> array =this.shoeTypeAndManufacturingRequestsarrayList.get(shoeType);
				logger.config ("39-managservice");
				ManufacturingOrderRequest manforderR = array.get(array.size()-1);
				logger.config ("40-managservice");		
				if (this.ManufacturingRequestAndNumberOfShoesAvailable.get(manforderR)==0){
					logger.info ("41-managservice- shoe "+shoeType+"~ does not appear in factory~, will create a new demand");
					CopyOnWriteArrayList<ManufacturingOrderRequest> arrayList = new CopyOnWriteArrayList<ManufacturingOrderRequest>();
					logger.config ("42-managservice");
					ManufacturingOrderRequest newManufactureOrderR = new ManufacturingOrderRequest(shoeType, this.currTick%5 +1 ,this.currTick);
					logger.config ("43-managservice");
					arrayList.add(newManufactureOrderR);
					logger.info ("44-managservice - put manufactureOrderR in the 3 hashmaps");
					shoeTypeAndManufacturingRequestsarrayList.put(shoeType,arrayList);
					logger.config ("15-managservice");
					this.ManufacturingRequestAndNumberOfShoesAvailable.put(newManufactureOrderR, this.currTick%5);
					logger.config ("16-managservice");
					CopyOnWriteArrayList<RestockRequest> list = new CopyOnWriteArrayList<RestockRequest>();
					logger.config ("17-managservice");
					list.add(r);
					logger.config ("18-managservice");
					this.ManufacturingRequestAndRestockRequest.put(newManufactureOrderR, list);
					logger.config ("19-managservice");
					//here
					ManagerSenNewManufactureOrderRequest(newManufactureOrderR);
					
				}
				
				else{
			
					logger.info ("65-managservice - there are reserves left, no need to send a new manufRequest. shoe = "+shoeType);
					int amountOfShoe = this.ManufacturingRequestAndNumberOfShoesAvailable.get(manforderR);
					logger.config ("66-managservice");
					this.ManufacturingRequestAndNumberOfShoesAvailable.replace(manforderR, amountOfShoe-1);
					logger.severe("part B.2 ----------there are reserves left, no need to send a new manufRequest-------   shoe ="+shoeType+"hashmap2NewAmount for manuf = "+(amountOfShoe-1 ));
					logger.config ("67-managservice");
					this.ManufacturingRequestAndRestockRequest.get(manforderR).add(r);
				}
			}			
		
		});	
		
		logger.config ("68-managservice");
		subscribeBroadcast(TerminateBroadcast.class, callback->{logger.severe("manager terminated"); terminate();});   
		latchObject.countDown();
   }
	

	

	
	
	
	
	
	
}
