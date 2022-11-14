import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountPageComponent } from './account-page/account-page.component';
import { RaidPageComponent } from './raid-page/raid-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { AccountDetailsComponent } from './account-page/account-details/account-details.component';
import { AccountOverviewComponent } from './account-page/account-overview/account-overview.component';
import { RequestFundsComponent } from './account-page/request-funds/request-funds.component';
import { TransferFundsFormComponent } from './account-page/transfer-funds-form/transfer-funds-form.component';
import { PurchaseTroopsComponent } from './raid-page/purchase-troops/purchase-troops.component';

const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'account', component: AccountPageComponent},
  {path: 'home', component: MainPageComponent},
  {path: 'raid', component: RaidPageComponent},
  {path: 'register', component: RegistrationFormComponent},
  {path: 'login', component: LoginFormComponent},
  {path: 'account/details/', component: AccountDetailsComponent},
  {path: 'account/overview/', component: AccountOverviewComponent},
  {path: 'account/requestfunds/', component: RequestFundsComponent},
  {path: 'account/transferfunds/', component: TransferFundsFormComponent},
  {path: 'raid/purchasetroops/', component: PurchaseTroopsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
