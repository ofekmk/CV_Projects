package bgu.spl.mics.impl;
import java.util.concurrent.LinkedBlockingQueue;

import bgu.spl.mics.Broadcast;
import bgu.spl.mics.Message;
import bgu.spl.mics.MessageBus;
import bgu.spl.mics.MicroService;
import bgu.spl.mics.Request;
import bgu.spl.mics.RequestCompleted;

import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.ConcurrentHashMap;
import java.util.Iterator;
import java.util.Map;


/**
 * The message-bus is a shared object used for communication between
 * micro-services.
 * It should be implemented as a thread-safe singleton.
 * The message-bus implementation must be thread-safe as
 * it is shared between all the micro-services in the system.
 */
public class MessageBusImpl implements MessageBus {
	private static final Exception IllegalStateException = null;
	ConcurrentHashMap<Request,MicroService> requestsHashMap;
	ConcurrentHashMap<MicroService,LinkedBlockingQueue<Message>> microServicesHashMap;
	ConcurrentHashMap<Class<? extends Message>,LinkedBlockingQueue<MicroService>> broadCastMessagesTypesHashMap;
	ConcurrentHashMap<Class<? extends Message>,RoundRobinController> requestsMessagesTypesHashMap;
	    

	    private static class MessageBusImplObjHolder { 
	        public static  MessageBusImpl instance = new MessageBusImpl();
	    }
	    
	    private MessageBusImpl() {
	    	microServicesHashMap = new ConcurrentHashMap<MicroService,LinkedBlockingQueue<Message>>();
	    	requestsHashMap = new ConcurrentHashMap<Request,MicroService>();
	        broadCastMessagesTypesHashMap = new ConcurrentHashMap<Class<? extends Message>,LinkedBlockingQueue<MicroService>>();
	        requestsMessagesTypesHashMap = new ConcurrentHashMap<Class<? extends Message>,RoundRobinController>();
	    }
	    
	    
	public static MessageBusImpl getInstance() {
	        return MessageBusImplObjHolder.instance;
	    }
	protected ConcurrentHashMap<MicroService,LinkedBlockingQueue<Message>> getmicroServicesHashMap(){
		return microServicesHashMap;
	}
	
	
	 @Override
	 public  void subscribeRequest(Class<? extends Request> type, MicroService m){

	    	
	    if (this.requestsMessagesTypesHashMap.get(type)==null){
	    	RoundRobinController rb = new RoundRobinController();
	    	CopyOnWriteArrayList<MicroService> array = new CopyOnWriteArrayList<MicroService>();
	    	array.add(m);
	    	rb.setPointer(0);
	    	rb.setArray(array);
	    	this.requestsMessagesTypesHashMap.put(type, rb);
	    }
	    else{
	    	
	    	CopyOnWriteArrayList<MicroService> array =null;
    	array = this.requestsMessagesTypesHashMap.get(type).getArray();
		array.add(m);
	    }
    }
	    
	    
	    @Override
	 public synchronized void subscribeBroadcast(Class<? extends Broadcast> type, MicroService m){
	
	  	    if (this.broadCastMessagesTypesHashMap.get(type)==null){
	  	    	LinkedBlockingQueue<MicroService> q = new LinkedBlockingQueue<MicroService>();
	  	    	try {
					q.put(m);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
	  	    	this.broadCastMessagesTypesHashMap.put(type, q);
	  	    }
	  	    else{
	  	    	
	      	LinkedBlockingQueue<MicroService> typeQueue=null;
	      	typeQueue = this.broadCastMessagesTypesHashMap.get(type);
	  		typeQueue.add(m);
	  	    }
	      }
@Override
     public <T> void complete(Request<T> r, T result){
	    if (requestsHashMap.containsKey(r)){
    	RequestCompleted<T> comp = new RequestCompleted<T>(r,result);
    	MicroService ms = this.requestsHashMap.get(r); 
    	if (this.microServicesHashMap.containsKey(ms))
    		this.microServicesHashMap.get(ms).add(comp);
    	this.requestsHashMap.remove(r);
	    }
    	
    }

@Override
    
 public void sendBroadcast(Broadcast b){
         
	if (this.broadCastMessagesTypesHashMap.get(b.getClass())!=null){
	       for (MicroService it:this.broadCastMessagesTypesHashMap.get(b.getClass())){
				this.microServicesHashMap.get(it).add(b);
			
    	   
    	}
    }
}

@Override
   public synchronized boolean sendRequest(Request<?> r, MicroService requester){ 
	
	  	if (this.requestsMessagesTypesHashMap.get(r.getClass())!=null){
    	if (!this.requestsMessagesTypesHashMap.get(r.getClass()).getArray().isEmpty()){
         RoundRobinController rb = this.requestsMessagesTypesHashMap.get(r.getClass());
         int indToAdd = rb.getPointer();
         MicroService msToAdd = rb.getArray().get(indToAdd);
         this.requestsHashMap.put(r, requester);
         this.microServicesHashMap.get(msToAdd).add(r);//
         

         if (!(rb.array.size()==0))
         rb.setPointer((indToAdd+1)%(rb.getArray().size()));
    	return true;	
    }
	  	}
    	return false;
}
    	
    	
    	@Override
   public void register(MicroService m){
      
    	this.microServicesHashMap.put(m, new LinkedBlockingQueue<Message>());  	
    }
    
    
    	@Override      
    public synchronized void unregister(MicroService m){
    		for(Class<? extends Message> it1:this.requestsMessagesTypesHashMap.keySet()){
    			for (int i=0;i<this.requestsMessagesTypesHashMap.get(it1).getArray().size();i++){
    				if (m==this.requestsMessagesTypesHashMap.get(it1).getArray().get(i)){
    					 this.requestsMessagesTypesHashMap.get(it1).getArray().remove(i);
    					 if (this.requestsMessagesTypesHashMap.get(it1).getPointer()>=i)
    						 this.requestsMessagesTypesHashMap.get(it1).setPointer(this.requestsMessagesTypesHashMap.get(it1).getPointer()-1); 
    				}
    					
    				
            	   
    		}
    			}

    		for(Class<? extends Message> it1:this.broadCastMessagesTypesHashMap.keySet()){
    			for (MicroService it2:this.broadCastMessagesTypesHashMap.get(it1))
    				if (m==it2)
            	    this.broadCastMessagesTypesHashMap.get(it1).remove(it2);
    		}
   	
    	if(!(this.requestsHashMap.isEmpty())){
    	for (Request it:this.requestsHashMap.keySet()){ 
    	  if (this.requestsHashMap.get(it)==m)
    		  this.requestsHashMap.remove(it);
    	}
    	
    	}
    	this.microServicesHashMap.remove(m);
    		
      	}
   
    	@Override
   public Message awaitMessage(MicroService m) throws InterruptedException{
    

				
            	if (!(this.microServicesHashMap.containsKey(m))){
            		throw new IllegalStateException();
            	}
            return this.microServicesHashMap.get(m).take();
}
}

    	
    	



