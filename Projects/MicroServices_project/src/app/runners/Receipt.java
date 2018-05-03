package app.runners;


/**
 * 
 * Receipt is an object that plays a role of the receipt that the factoryService/sellingService sends back to the buyer.
 *<p>
 *
 *@param seller - the seller's name (Factory/SellingService)
 *@param customer - The cusomer's name(manager/webclientService)
 *@param shoeType - the shoeType requested
 *@param discount -  Boolean value to know whether the requested shoe was purchased in discount or not
 *@param issuedTick - issued tick 
 *@param requestTick - requested tick
 *@param amountSold - amount sold of the shoe
 */

public class Receipt{
	String seller;
	String customer;
	String shoeType;
	boolean discount;
	int issuedTick;
	int requestTick;
	int amountSold;
	
	//sync? methods needed? i don't think so 
	public Receipt(String seller,String customer ,String shoeType, boolean discount,
		int issuedTick, int requestTick, int amountSold) {
		this.seller = seller;
		this.customer= customer;
		this.shoeType = shoeType;
		this.discount = discount;
		this.issuedTick = issuedTick;
		this.requestTick = requestTick;
		this.amountSold = amountSold;
	}

	public String getCustomer() {
		return customer;
	}

	public void setCustomer(String customer) {
		this.customer = customer;
	}

	@Override
	public String toString() {//sync?
		return (
			"Seller: " + getSeller() + "\n"+"customer:"+this.getCustomer() +"\n"+ "Shoe Type: " + getShoeType() +"\n"+ "Was the shoe discounted: " + isDiscount()
			+ "\n" + "Requested tick: " + getRequestTick() + "\n" + "Issued tick: " +getIssuedTick() + "\n" + "Amount sold: " + getAmountSold());
	}

	public String getSeller() {
		return seller;
	}

	public void setSeller(String seller) {
		this.seller = seller;
	}

	public String getShoeType() {
		return shoeType;
	}

	public void setShoeType(String shoeType) {
		this.shoeType = shoeType;
	}

	public boolean isDiscount() {
		return discount;
	}

	public void setDiscount(boolean discount) {
		this.discount = discount;
	}

	public int getIssuedTick() {
		return issuedTick;
	}

	public void setIssuedTick(int issuedTick) {
		this.issuedTick = issuedTick;
	}

	public int getRequestTick() {
		return requestTick;
	}

	public void setRequestTick(int requestTick) {
		this.requestTick = requestTick;
	}

	public int getAmountSold() {
		return amountSold;
	}

	public void setAmountSold(int amountSold) {
		this.amountSold = amountSold;
	}
	
}
