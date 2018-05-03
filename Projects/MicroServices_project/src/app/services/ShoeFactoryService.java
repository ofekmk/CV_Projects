package app.services;


import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.logging.Logger;

import app.message.ManufacturingOrderRequest;
import app.message.TerminateBroadcast;
import app.message.TickBroadcast;
import app.runners.Receipt;
import bgu.spl.mics.MicroService;


/**
 * 
 * ShoeFactoryService is responsible to complete {@link ManufacturingOrderRequest} to the manager.
 * <P>
 * @param Strinameng - name of factory
 *@param manufReqQueue -  Queue of given {@link ManufacturingOrderRequest}
 *@param currAmountLeftForCurrentManufReq - How many shoes are left to produce from the current {@link ManufacturingOrderRequest}
 *@param currTick - current tick
 *@param currManufReq - current {@link ManufacturingOrderRequest} that is being taken care
 *@param latchObj -  for correct timing
 */

public class ShoeFactoryService extends MicroService{
	String name;
	ConcurrentLinkedQueue<ManufacturingOrderRequest> manufReqQueue;
	private CountDownLatch latchObj;
	private int currAmountLeftForCurrentManufReq;
	private AtomicInteger currTick;
	private ManufacturingOrderRequest currManufReq;
	
	
	
	/**
	 * ShoeFactoryService constructor
	 * 
	 */
	
	public ShoeFactoryService(String name, CountDownLatch countDownLatchObject) {
		super(name);
		manufReqQueue= new ConcurrentLinkedQueue<ManufacturingOrderRequest>();
		latchObj= countDownLatchObject;
		currAmountLeftForCurrentManufReq= 0;
		currManufReq=null;
		currTick = new AtomicInteger();
	}
	
	
	
	
	/**
	 * The ShoeFactoryService subscribes to {@link TickBroadcast} , and for each tick he checks if he has a shoe to produce(1 shoe per tick).
	 * first he checks if the currManufReq is not null. else, he will see if there are any {@link ManufacturingOrderRequest} left in it's {@value manufReqQueue}
	 * 
	 * The ShoeFactoryService is also subscribed to {@link ManufacturingOrderRequest} - to add it to the tail of it's {@value manufReqQueue}
	 */
	
	@Override
	protected void initialize() {
		Logger logger = Logger.getLogger("abc");
		subscribeBroadcast(TickBroadcast.class, onComplete->{
			this.currTick.set(onComplete.getCurrTick());
			if (this.currManufReq!=null){
				if (this.currAmountLeftForCurrentManufReq==0){
						Receipt rec = new Receipt(this.getName(),"Manager", currManufReq.getShoeType(),false,this.currTick.get(), currManufReq.getTick(),currManufReq.getAmount());
						complete(currManufReq,rec);
						logger.severe("factory "+this.getName()+"completed ManufR . amount completed= " + this.currManufReq.getAmount()+" shoe = "+this.currManufReq.getShoeType());
						this.currManufReq=null;
						if (!(this.manufReqQueue.isEmpty())){
							this.currManufReq=this.manufReqQueue.poll();
							this.currAmountLeftForCurrentManufReq=(currManufReq.getAmount()-1);
						}
				}
				else{
					this.currAmountLeftForCurrentManufReq=this.currAmountLeftForCurrentManufReq-1;
				}
			}
			else{
				if (!this.manufReqQueue.isEmpty()){
					this.currManufReq=this.manufReqQueue.poll();
					this.currAmountLeftForCurrentManufReq=this.currManufReq.getAmount()-1;
				}
			}
				

		});
		
		subscribeRequest(ManufacturingOrderRequest.class, onComplete->{
			logger.severe("Factory" +this.getName()+" got an order of " +onComplete.getAmount()+" "+onComplete.getShoeType());
		    this.manufReqQueue.add(onComplete);
		});
		
		subscribeBroadcast(TerminateBroadcast.class, callback->{logger.severe("shoeFACTORY terminate"); terminate();});    
		latchObj.countDown();
	}
}
		
		
	