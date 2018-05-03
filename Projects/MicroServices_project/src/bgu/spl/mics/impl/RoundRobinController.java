package bgu.spl.mics.impl;
import java.util.concurrent.CopyOnWriteArrayList;

import bgu.spl.mics.MicroService;
import bgu.spl.mics.impl.MessageBusImpl;
/**
 * The RoundRobinController is an object that takes a role of controlling the round-robin fashion
 * for MicroServices that are signed to the {@link requestsMessagesTypesHashMap}. it Will serve as the value for this
 * map, contacting a pointer field, and an arraylist of Microservices.
 * 
 */



public class RoundRobinController {
	
	protected CopyOnWriteArrayList<MicroService> array;
	protected int pointer;
	
	/**
	 * constructor containing the following parameters:
	 * <P>
	 * @param array - array of the microservices 
	 * @param pointer - points to which microservice should get the next given message
	 */
	
	protected RoundRobinController(){
		this.array =  new CopyOnWriteArrayList<MicroService>();
		this.pointer = -1;
	}
	
	
	protected CopyOnWriteArrayList<MicroService> getArray() {
		return this.array;
	}
	
	protected int getPointer(){
		return this.pointer;
	}
	
	
	protected void setArray(CopyOnWriteArrayList<MicroService> arr){
		this.array = arr;
	}
	
	
	protected void setPointer(int p){
	this.pointer = p;

}
}
