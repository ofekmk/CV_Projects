package app.message;

import bgu.spl.mics.Broadcast;

public class TickBroadcast implements Broadcast{
	private int currTick;

	public TickBroadcast(int currTick) {
		this.currTick = currTick;
	}

	public int getCurrTick() {
		return currTick;
	}

	public void setCurrTick(int currTick) {
		this.currTick = currTick;
	}

	@Override
	public String toString() {
		return "TickBroadcast [Current tick time=" + currTick + "]";
	}
	
}
