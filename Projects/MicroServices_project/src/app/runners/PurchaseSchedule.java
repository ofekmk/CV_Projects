package app.runners;

/**
 * 
 * PurchaseSchedule holds the tick and shoe that the WebClientService will send a \\purchReq
 *<p>
 *shoeType - the shoetype that the buyer wants to buy
 *tick - the tick that the buyer wants the buy the shoe
 *
 */

public class PurchaseSchedule {
	private String shoeType;
	private int tick;
	
	public PurchaseSchedule(String shoeType, int tick) {
		this.shoeType = shoeType;
		this.tick = tick;
	}

	public String getShoeType() {
		return shoeType;
	}

	public void setShoeType(String shoeType) {//sync?
		this.shoeType = shoeType;
	}

	public int getTick() {
		return tick;
	}

	public void setTick(int tick) {//sync?
		this.tick = tick;
	}
	
	
}
