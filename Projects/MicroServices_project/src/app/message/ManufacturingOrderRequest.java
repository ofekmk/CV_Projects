package app.message;

import app.runners.Receipt;
import bgu.spl.mics.Request;

public class ManufacturingOrderRequest implements Request<Receipt> {
	private String shoeType;
	private int amount;
	private int tick;
	
	public ManufacturingOrderRequest(String shoeType, int amount, int tick) {
		this.shoeType = shoeType;
		this.amount = amount;
		this.tick = tick;
	}
	
	public int getTick(){
		return this.tick;
	}
	
	public void setTick(int tick){
		this.tick=tick;
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
	
	
}
