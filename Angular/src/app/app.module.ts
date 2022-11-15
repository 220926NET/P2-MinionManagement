import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './login/login.component';
import { HttpClientModule} from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// Angular version Responsive web design
import { FlexLayoutModule } from '@angular/flex-layout';
// Angualr material Form 
import {MatFormFieldModule} from '@angular/material/form-field';
// Angular material Input
import {MatInputModule} from '@angular/material/input';
// Angular material Button
import {MatButtonModule} from '@angular/material/button';
// Angualr material Card
import {MatCardModule} from '@angular/material/card';
// Angualr material Toolbar
import {MatToolbarModule} from '@angular/material/toolbar';
// Angular material icon
import {MatIcon, MatIconModule} from '@angular/material/icon'

import { RegisterComponent } from './register/register.component';
import { TransactionComponent } from './transaction/transaction.component';
import { AdminComponent } from './admin/admin.component';

import { AccountPageComponent } from './account-page/account-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { TransferFundsFormComponent } from './account-page/transfer-funds-form/transfer-funds-form.component';
import { AccountDetailsComponent } from './account-page/account-details/account-details.component';
import { AccountOverviewComponent } from './account-page/account-overview/account-overview.component';
import { RequestFundsComponent } from './account-page/request-funds/request-funds.component';
import { PurchaseTroopsComponent } from './raid-page/purchase-troops/purchase-troops.component';
import { RaidPageComponent } from './raid-page/raid-page.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    TransactionComponent,
    AccountPageComponent,
    MainPageComponent,
    NavBarComponent,
    UserProfileComponent,
    TransferFundsFormComponent,
    AccountDetailsComponent,
    AccountOverviewComponent,
    RequestFundsComponent,
    PurchaseTroopsComponent,
    RaidPageComponent,
    AdminComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
