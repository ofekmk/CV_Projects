package bgu.spl.mics.impl;

import static org.junit.Assert.*;
import org.junit.BeforeClass;
import org.junit.Test;
import app.message.ManufacturingOrderRequest;
import app.message.NewDiscountBroadcast;
import app.message.PurchaseOrderRequest;
import app.message.RestockRequest;
import app.message.TerminateBroadcast;
import app.message.TickBroadcast;
import app.runners.Receipt;
import bgu.spl.mics.Broadcast;
import bgu.spl.mics.MicroService;

public class MessageBusImplTest {
	private static MessageBusImpl messageBus;
    @BeforeClass
   
    
    public static void setUpBeforeClass() throws Exception {
            messageBus = MessageBusImpl.getInstance();    
    }

	@Test
	public void testAwaitMessage() {
		MicroService takingCareAwaitMessage = new MicroService("takingCareAwaitMessage") {
			protected void initialize() {}};
        messageBus.register(takingCareAwaitMessage);
		TickBroadcast tick=new TickBroadcast (10);
		messageBus.subscribeBroadcast(tick.getClass(), takingCareAwaitMessage);
		messageBus.sendBroadcast(tick);
		try {
			assertTrue(messageBus.awaitMessage(takingCareAwaitMessage)==tick);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
        messageBus.unregister(takingCareAwaitMessage);
	}
   

	@Test
	public void testSubscribeBroadcast() {
		MicroService takingCare = new MicroService("takingCare") {
			protected void initialize() {
			}
		};
		messageBus.subscribeBroadcast(NewDiscountBroadcast.class, takingCare);
	}

	@Test
	public void testComplete() {
		MicroService takingCare = new MicroService("takingCare") {
			protected void initialize() {
			}
		};
		MicroService requester = new MicroService("requester") {
			protected void initialize() {
			}
		};
		ManufacturingOrderRequest msg = new ManufacturingOrderRequest("costumer", 7, 3);
		Receipt receiptComplete= new Receipt("Seller", "Store ", msg.getShoeType(), false , 7, msg.getTick(), 3);
		messageBus.register(takingCare);
		messageBus.register(requester);
		messageBus.subscribeRequest(PurchaseOrderRequest.class, takingCare);
		messageBus.sendRequest(msg, requester);
		messageBus.complete(msg, receiptComplete);
		messageBus.unregister(takingCare);
		messageBus.unregister(requester);
	}

	@Test
	public void testSendBroadcast() {
		 MicroService sendBrod = new MicroService("requester") {protected void initialize() {}};
         TerminateBroadcast broad1 = new TerminateBroadcast() {};
         messageBus.subscribeBroadcast(broad1.getClass(), sendBrod);
         Broadcast broad2 = new Broadcast() {};
         messageBus.sendBroadcast(broad2);
	}

	@Test
	public void testSendRequest() {
		MicroService takingCareSendRequest = new MicroService("takingCareSendRequest") {
			protected void initialize() {
			}
		};
		MicroService requesterSendRequest = new MicroService("requesterSendRequest") {
			protected void initialize() {
			}
		};
		RestockRequest msgSendReq = new RestockRequest("costumerSendRequest", "BENTZI", 3);
		messageBus.register(takingCareSendRequest);
		messageBus.subscribeRequest(RestockRequest.class, takingCareSendRequest);
		assertTrue(messageBus.sendRequest(msgSendReq, requesterSendRequest));
		messageBus.unregister(takingCareSendRequest);
	}

	@Test
	public void testSubscribeRequest(){
		MicroService subReq = new MicroService("subReq") {
			protected void initialize() {}};
		RestockRequest newRequest = new RestockRequest("Adidas","aaa", 3);
		messageBus.subscribeRequest(newRequest.getClass(), subReq);
		messageBus.unregister(subReq);
	}
	
	@Test
	public void testRegister() {
		MicroService takingCareRegister = new MicroService("takingCare") {
			protected void initialize() {
			}
		};
		assertFalse(messageBus.getInstance().getmicroServicesHashMap().containsKey(takingCareRegister));
		messageBus.register(takingCareRegister);
		assertTrue(messageBus.getInstance().getmicroServicesHashMap().containsKey(takingCareRegister));
	}

	@Test
	public void testUnregister() {
		MicroService takingCare22 = new MicroService("takingCare22") {
			protected void initialize() {
			}
		};
		MicroService requester = new MicroService("requester") {
			protected void initialize() {
			}
		};
		messageBus.register(takingCare22);
		assertTrue(messageBus.getInstance().getmicroServicesHashMap().containsKey(takingCare22));
		messageBus.unregister(takingCare22);
		assertFalse(messageBus.getInstance().getmicroServicesHashMap().containsKey(takingCare22));
	}



}
