package app.runners;

/**
 * DiscountSchedule holds the tick and shoe that the manager will send as a broadcast message
 * <p>
 * @param shoeType - the shoe type
 *@param tick - the tick to send the message
 *@param amount - the amount of shoes to put in discount
 */

public class DiscountSchedule {
	String shoeType;
	int tick;
	int amount;
	
	public DiscountSchedule(String shoeType, int tick, int amount) {
		this.shoeType = shoeType;
		this.tick = tick;
		this.amount = amount;
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

	public void setTick(int tick) {//syhnc?
		this.tick = tick;
	}

	public int getAmount() {
		return amount;
	}

	public void setAmount(int amount) {//sync?
		this.amount = amount;
	}
}

