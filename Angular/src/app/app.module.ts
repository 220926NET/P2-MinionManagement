import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AccountPageComponent } from './account-page/account-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { TransferFundsFormComponent } from './account-page/transfer-funds-form/transfer-funds-form.component';
import { AccountDetailsComponent } from './account-page/account-details/account-details.component';
import { AccountOverviewComponent } from './account-page/account-overview/account-overview.component';
import { RequestFundsComponent } from './account-page/request-funds/request-funds.component';
import { PurchaseTroopsComponent } from './raid-page/purchase-troops/purchase-troops.component';
import { RaidPageComponent } from './raid-page/raid-page.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountPageComponent,
    MainPageComponent,
    NavBarComponent,
    UserProfileComponent,
    RegistrationFormComponent,
    LoginFormComponent,
    TransferFundsFormComponent,
    AccountDetailsComponent,
    AccountOverviewComponent,
    RequestFundsComponent,
    PurchaseTroopsComponent,
    RaidPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
