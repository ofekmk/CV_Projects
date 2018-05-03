package app.runners;

import java.util.concurrent.atomic.AtomicInteger;



/**
 * 
 *ShoeStorageInfo is an object that hold the shoe's information 
 *<p>
 *@param shoeType - the ShoeType
 *	@param amountOnStorage - the amount of this specific shoe in the sore
 *	@param discountedAmount - the amount of this shoe in this store that are discounted.
 *
 * 
 */


public class ShoeStorageInfo {
	private String shoeType;
	private AtomicInteger amountOnStorage;
	private AtomicInteger discountedAmount;
	
	public ShoeStorageInfo(String shoeType, int amountOnStorage,int discountedAmount) {
		this.shoeType = shoeType;
		this.amountOnStorage= new AtomicInteger(amountOnStorage);
		this.discountedAmount= new AtomicInteger(discountedAmount);
	}
	
	@Override
	public String toString() { 
		return ("**************Shoe Info	**************"
			+ "\n" +"Shoe Name: " + getShoeType() + "\n" +  
					"Amount: " + getAmountOnStorage() + "\n"
					+ "Discounted Amount: " + getDiscountedAmount());
	}

	public String getShoeType() {
		return shoeType;
	}
	public void setShoeType(String shoeType) {
		this.shoeType = shoeType;
	}
	public int getAmountOnStorage() {
		return amountOnStorage.get();
	}
	public void setAmountOnStorage(int amountOnStorage) { 
		this.amountOnStorage.set(amountOnStorage); 
	}
	public int getDiscountedAmount() {
		return discountedAmount.get();
	}
	public void setDiscountedAmount(int discountedAmount) {
		this.discountedAmount.set(discountedAmount); 
	}

}
