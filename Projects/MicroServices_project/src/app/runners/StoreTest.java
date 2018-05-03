package app.runners;

import static org.junit.Assert.assertEquals;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import app.runners.Store.BuyResult;

/**
 * junit test for the Store Class
 * @author Ofek Kettner
 *
 */

public class StoreTest {

	private  Store store;
	
	@Before
	public void setUp() throws Exception {
		store = Store.getInstance();
		ShoeStorageInfo shoeTypeA = new ShoeStorageInfo("AHRON",1, 0);
		ShoeStorageInfo shoeTypeB = new ShoeStorageInfo("BRISTOL",4, 0);
		ShoeStorageInfo shoeTypeC = new ShoeStorageInfo("CHAMRO",5, 0);
		ShoeStorageInfo shoeTypeD = new ShoeStorageInfo("DAVID",6, 0);

		ShoeStorageInfo[] shoes= new ShoeStorageInfo[4];
		shoes[0]=shoeTypeA;
		shoes[1]=shoeTypeB;
		shoes[2]=shoeTypeC;
		shoes[3]=shoeTypeD;
		store.load(shoes);
		store.print();
	}

	@After
	public void tearDown() throws Exception {
		store.print();
	}

	@Test
	public void testLoad() {
		assertEquals(store.take("BRISTOL", false), BuyResult.REGULAR_PRICE);
		assertEquals(store.take("GOCHI", false), BuyResult.NOT_IN_STOCK); 
	}

	@Test
	public void testTake() {
		assertEquals(store.take("AHRON", false), BuyResult.REGULAR_PRICE);
		assertEquals(store.take("AHRON", false), BuyResult.NOT_IN_STOCK);
		assertEquals(store.take("BRISTOL", true), BuyResult.NOT_ON_DISCOUNT);
		store.addDiscount("BRISTOL", 2);
		assertEquals(store.take("BRISTOL", true), BuyResult.DISCOUNTED_PRICE);
		assertEquals(store.take("BRISTOL", false), BuyResult.DISCOUNTED_PRICE);
	}
	

	@Test
	public void testAdd() {
		store.add("BENTZI", 1);
		assertEquals(store.take("BENTZI", false) ,BuyResult.REGULAR_PRICE);
		assertEquals(store.take("BENTZI", false), BuyResult.NOT_IN_STOCK);
	}

	@Test
	public void testAddDiscount() {
		assertEquals(store.getStoreStorage().get("DAVID").getDiscountedAmount(), 0);
		assertEquals(store.take("DAVID", true), BuyResult.NOT_ON_DISCOUNT);
		store.addDiscount("DAVID", 2);
		assertEquals(store.take("DAVID", false), BuyResult.DISCOUNTED_PRICE);
		assertEquals(store.take("DAVID", true), BuyResult.DISCOUNTED_PRICE);
		assertEquals(store.take("DAVID", false), BuyResult.REGULAR_PRICE);
		assertEquals(store.take("DAVID", true), BuyResult.NOT_ON_DISCOUNT);
	}	
	
}
