describe('Clients buys from service with two Brokers', function(){
	var broker1,
		broker2,
		clientA,
		clientB,
		clientC,
		service;
	
	
	beforeEach(function(){
		broker1 = new Broker(0.05, 1.49);
		var commissionArray = [
			{'range': 40, 'commission': 0.03},
			{'range': 80, 'commission': 0.025},
			{'range': 100, 'commission': 0.02},	
		];
		
		broker2 = new Broker(commissionArray, 1.52);
		clientA = new Client('ClientA');
		clientB = new Client('ClientB');
		clientC = new Client('ClientC');
		
		service = new BrokerageService();
		service.registerBroker(broker1);
		service.registerBroker(broker2);
	});
	
	it('ClientA buys 10 for 15.645', function(){
		expect(  clientA.makeOrder(10, service)).toBe(15.645);
	});
	
	it('ClientB buys 40 for 62.58', function(){
		expect( clientB.makeOrder(40, service)).toBe(62.58);
	});
	
	it('ClientA buys 50 for 77.9', function(){
		expect( clientB.makeOrder(50, service)).toBe(77.9);
	});
	
	it('ClientB buys 100 for 155.04', function(){
		expect( clientB.makeOrder(100, service)).toBe(155.04);
	});
	
	it('ClientB sells 80 for 124.64', function(){
		expect( clientB.makeOrder(80, service)).toBe(124.64);
	});
	
	it('ClientA sells 70 for 109.06', function(){
		expect( clientC.makeOrder(70, service)).toBe(109.06);
	});
		
	it('ClientA buys 130 for 201.975', function(){
		expect( clientA.makeOrder(130, service)).toBe(201.975);
	});
		
	it('ClientB buys 60 for 93.48', function(){
		expect( clientB.makeOrder(50, service)).toBe(93.48);
	});
	
});
