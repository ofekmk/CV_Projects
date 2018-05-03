package app.message;

import java.util.concurrent.atomic.AtomicInteger;

import app.runners.Receipt;
import bgu.spl.mics.Request;

public class PurchaseOrderRequest implements Request<Receipt>{
	private String customer;
	private String shoeType;
	private int amount;
	private boolean onlyDiscount;
	private AtomicInteger requestTick;
	
	public PurchaseOrderRequest(String customer, String shoeType,boolean onlyDiscount,int tick) {
		super();
		this.customer = customer;
		this.shoeType = shoeType;
		this.onlyDiscount = onlyDiscount;
		this.requestTick=new AtomicInteger(tick); 
	}
	
	public int getRequestTick() {
		return requestTick.get();
	}
	
	
	public void setRequestTick(int requestTick) {
		this.requestTick.set(requestTick); 
	}
	
	
	
	

	
	
	
	
	public String getCustomer() {
		return customer;
	}
	
	
	public void setCustomer(String customer) {
		this.customer = customer;
	}
	public String getShoeType() {
		return shoeType;
	}
	public void setShoeType(String shoeType) {
		this.shoeType = shoeType;
	}
	public int getAmount() {
		return amount;
	}
	public void setAmount(int amount) {
		this.amount = amount;
	}
	public boolean isOnlyDiscount() {
		return onlyDiscount;
	}
	public void setOnlyDiscount(boolean onlyDiscount) {
		this.onlyDiscount = onlyDiscount;
	}
}
