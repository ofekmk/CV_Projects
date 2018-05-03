package app.message;

import bgu.spl.mics.Request;

public class RestockRequest implements Request<Boolean>{
	private String shoeType;
	private int reqTime;
	private String seller;
	
	public RestockRequest(String shoeType, String seller,int reqTime ) {
		this.shoeType = shoeType;
		this.reqTime = reqTime;
		this.seller = seller;
	}

	public String getShoeType() {
		return shoeType;
	}

	public void setShoeType(String shoeType) {
		this.shoeType = shoeType;
	}	
	public int getReqTime(){
		return this.reqTime;
	}
	
	public String getSeller(){
		return this.seller;
	}
}
