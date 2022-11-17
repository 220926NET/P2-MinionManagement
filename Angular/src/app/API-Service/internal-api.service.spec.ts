import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController} from '@angular/common/http/testing'
import { InternalAPIService } from './internal-api.service';

describe('InternalAPIService', () => {
  let service: InternalAPIService;
  let httpMockController: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[
        HttpClientTestingModule
      ]
    });
    httpMockController = TestBed.inject(HttpTestingController);
    service = TestBed.inject(InternalAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  // testing Login function
  it('should send POST request to Login', () =>{

    const mockedRes : any = ["token:abcd"];


    service.Login("username and password").subscribe((res) =>{
      expect(res).toBeTruthy();
      expect(res).toEqual(mockedRes);
    })

    const req = httpMockController.expectOne(
      'https://minionmanagement.azurewebsites.net/Authentication/Login'
      );
    expect(req.request.method).toBe('POST');


    req.flush(mockedRes);
    httpMockController.verify();
  });

  // testing Register function
  it('should send POST request to Register', () => {

    const mockedRes : number = 201;

    service.Register("username and password").subscribe((res) => {
      expect(res).toEqual(mockedRes);
    })

    const req = httpMockController.expectOne(
      'https://minionmanagement.azurewebsites.net/Authentication/Register'
    );
    expect(req.request.method).toBe('POST');

    req.flush(mockedRes);
    httpMockController.verify();
  })

  // testing for transaction function
  it('should send POST request to Transaction', ()=>{
    
    const mockedRes : number = 201;
    service.Transaction({"from":1, "to":2, "amount":1}).subscribe((res) => {
      expect(res).toEqual(mockedRes);
    })

    const req = httpMockController.expectOne(
      "https://minionmanagement.azurewebsites.net/Account/Transaction"
    );
    expect(req.request.method).toBe('POST');

    req.flush(mockedRes);
    httpMockController.verify
  })

  //testing for AdminAddMoney
  it('should send POST request to AdminAddmoney', () =>{

    const mockedRes : number = 201;
    service.AdminAddMoney(1).subscribe((res) => {
      expect(res).toEqual(mockedRes);
    })

    const req = httpMockController.expectOne(
      "https://minionmanagement.azurewebsites.net/Admin/addmoney"
    );
    expect(req.request.method).toBe('POST');

    req.flush(mockedRes);
    httpMockController.verify;
  })

  //testing for AdminRemoveMoney
  it('should send POST request to AdminRemovemoney', () =>{

    const mockedRes : number = 201;
    service.AdminRemoveMoney(1).subscribe((res) => {
      expect(res).toEqual(mockedRes);
    })

    const req = httpMockController.expectOne(
      "https://minionmanagement.azurewebsites.net/Admin/removemoney"
    );
    expect(req.request.method).toBe('POST');

    req.flush(mockedRes);
    httpMockController.verify;
  })


  //testing BuyTroop 
  it('should send POST request to BuyTroop', () => {
    const mockedRes : number = 201;
    service.BuyTroop(1).subscribe((res) => {
      expect(res).toEqual(mockedRes);
    })

    const req = httpMockController.expectOne(
      "https://minionmanagement.azurewebsites.net/Account/buytroop"
    );
    expect(req.request.method).toBe('POST');

    req.flush(mockedRes);
    httpMockController.verify;
  })

  // testing Raid function
  it('should send GET request to Raid', () =>{
    const mockedRes : number = 201;
    service.Raid().subscribe((res) => {
      expect((res).toEqual(mockedRes))
    })

    const req = httpMockController.expectOne(
      "https://minionmanagement.azurewebsites.net/Account/Raid"
    );
    expect(req.request.method).toBe('GET');
    req.flush(mockedRes);
    httpMockController.verify;
  })

  //testing RaidResult
  it('should send OUT request to Raid', () => {
    const mockedRes : number = 200;
    service.RaidResult(1).subscribe((res) => {
      expect((res).toEqual(mockedRes))
    })

    const req = httpMockController.expectOne(
      "https://minionmanagement.azurewebsites.net/Account/Raid/1"
    );

    expect(req.request.method).toBe('PUT');

    req.flush(mockedRes);
    httpMockController.verify;
  })

  
  
  

});
