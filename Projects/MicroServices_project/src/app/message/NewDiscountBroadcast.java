package app.message;

import bgu.spl.mics.Broadcast;

public class NewDiscountBroadcast implements Broadcast{
	String shoeType;
	int amount;
	int currTick;
	
	public NewDiscountBroadcast (String shoeType, int amount, int currTick) {
		this.shoeType = shoeType;
		this.amount = amount;
		this.currTick= currTick;
	}

	public int getCurrTick() {
		return currTick;
	}

	public void setCurrTick(int currTick) {
		this.currTick = currTick;
	}

	public String  getShoeType() {
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

	@Override
	public String toString() {
		return "NewDiscountBroadcast [Shoe name: " + this.shoeType + ", amount of discounted items: " + amount
				+ "]";
	}
	
}
