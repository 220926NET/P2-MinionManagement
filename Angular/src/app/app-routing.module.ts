import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountPageComponent } from './account-page/account-page.component';
import { RaidPageComponent } from './raid-page/raid-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { AccountDetailsComponent } from './account-page/account-details/account-details.component';
import { AccountOverviewComponent } from './account-page/account-overview/account-overview.component';
import { RequestFundsComponent } from './account-page/request-funds/request-funds.component';
import { TransferFundsFormComponent } from './account-page/transfer-funds-form/transfer-funds-form.component';
import { PurchaseTroopsComponent } from './raid-page/purchase-troops/purchase-troops.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AdminComponent } from './admin/admin.component';
import { TransactionComponent } from './transaction/transaction.component';

const routes: Routes = [
  //set route to login component for <route-outlet>
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component: MainPageComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'raid', component: RaidPageComponent},
  {path: 'account', component: AccountPageComponent},
  {path: 'account/details', component: AccountDetailsComponent},
  {path: 'account/overview', component: AccountOverviewComponent},
  {path: 'account/requestfunds', component: RequestFundsComponent},
  {path: 'account/transferfunds', component: TransferFundsFormComponent},
  {path: 'raid/purchasetroops', component: PurchaseTroopsComponent},
  {path: 'userprofile', component: UserProfileComponent},
  {path: 'adminpanel', component: AdminComponent},
  {path: 'account/transaction', component: TransactionComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
