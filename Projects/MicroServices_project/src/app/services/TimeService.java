package app.services;


import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.CountDownLatch;
import java.util.logging.Logger;
import app.message.TerminateBroadcast;
import app.message.TickBroadcast;
import app.runners.Store;
import bgu.spl.mics.MicroService;

/**
 * 
 * The TimeService thread is responsible of sending {@link TickBroadcast} to all other services(threads)
 * it is responsible to send broadcast with {@link TerminateBroadcast} to all other threads, to notify them that they need to terminate(gracefuly termination)
 * <p>
 * @param - speed - the speed of each tick
 *@param - duration - duration of the program to run
 *@param - currentTime - current time
 *@param - timer 
 *@param countDownLatchObject - indication for this thread when to activate(when it's await gets 0)
 *
 */


public class TimeService extends MicroService {
		private int speed;
		private int duration;
		private int currentTime;
		public Timer timer;
		private CountDownLatch countDownLatchObject;
	
	public TimeService() {
		
			super("TimeService");
			this.speed=0;
			this.duration=0;
			this.currentTime=1;
			timer = new Timer();
	}
	
	public TimeService(int speed, int duration, CountDownLatch countDownLatchObject) {
			super("TimeService");
			this.speed=speed;
			this.duration=duration;
			this.currentTime=1;
			this.countDownLatchObject = countDownLatchObject;
			timer = new Timer();
	}

	
	
	
	
	@Override
	protected void initialize() {
		Logger logger = Logger.getLogger("abc");
		try {
			countDownLatchObject.await();
	    } 
		catch (InterruptedException e){
			e.printStackTrace();
	    }
		catch (NullPointerException ex){
			ex.printStackTrace();
	    }
		
		logger.info("--- All the threads are ready ---");

		timer.scheduleAtFixedRate(new TimerTask(){
			
			public void run() {
				TickBroadcast tickBroadcast = new TickBroadcast(currentTime);
				System.out.println();
				System.out.println("Current Time = "+ currentTime);
				sendBroadcast(tickBroadcast);

				currentTime=currentTime+1;
				if(currentTime==duration+1){
					timer.cancel();
					TerminateBroadcast terminate = new TerminateBroadcast();
					try{
						Thread.sleep(speed);
					}
					catch(InterruptedException e){
						e.printStackTrace();
					}
					finally {
						sendBroadcast(terminate);
						terminate();
						System.out.println();
						Store.getInstance().print();
						logger.warning("--- All the threads are done ---");
						
					}
				}
			}
		},0,speed);
		

		subscribeBroadcast(TickBroadcast.class, callback->{});
		subscribeBroadcast(TerminateBroadcast.class, callback->{logger.severe("TIMESERVICE terminated"); terminate();}); 
	}
	
	public int getCurrentTime(){
		return currentTime;
	}
	

}
