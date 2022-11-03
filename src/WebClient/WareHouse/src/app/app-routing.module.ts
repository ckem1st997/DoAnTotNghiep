import { HomeComponent } from './pages/home/home.component';
import { WareHouseBookComponent } from './pages/WareHouseBook/WareHouseBook.component';
import { WareHouseLimitComponent } from './pages/WareHouseLimit/WareHouseLimit.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WareHouseBenginingComponent } from './pages/WareHouseBengining/WareHouseBengining.component';
import { VendorComponent } from './pages/Vendor/Vendor.component';
import { FormsearchComponent } from './method/search/formsearch/formsearch.component';
import { WareHouseComponent } from './pages/WareHouse/WareHouse.component';
import { UnitComponent } from './pages/Unit/Unit.component';
import { WareHouseItemCategoryService } from './service/WareHouseItemCategory.service';
import { WareHouseItemCategoryComponent } from './pages/WareHouseItemCategory/WareHouseItemCategory.component';
import { WareHouseItemComponent } from './pages/WareHouseItem/WareHouseItem.component';
import { InwardCreateComponent } from './method/create/InwardCreate/InwardCreate.component';
import { NotFoundComponent } from './pages/NotFound/NotFound.component';
import { OutwardCreateComponent } from './method/create/OutwardCreate/OutwardCreate.component';
import { InwardEditComponent } from './method/edit/InwardEdit/InwardEdit.component';
import { InwardDetailsComponent } from './method/details/InwardDetails/InwardDetails.component';
import { OutwardDetailsComponent } from './method/details/OutwardDetails/OutwardDetails.component';
import { OutwardEditComponent } from './method/edit/OutwardEdit/OutwardEdit.component';
import { ReportTotalComponent } from './pages/ReportTotal/ReportTotal.component';
import { ReportDetalisComponent } from './pages/ReportDetalis/ReportDetalis.component';
import { LoginComponent } from './pages/login/login.component';
import { RoleUserComponent } from './pages/RoleUser/RoleUser.component';
import { DefaultLayoutComponent } from './layout/Default-Layout/Default-Layout.component';
import { AuthozireComponent } from './layout/Authozire/Authozire.component';
import { AuthGuard } from './extension/AuthGuard';
import { PagesForbieComponent } from './pages/PagesForbie/PagesForbie.component';
import { PagesOptionComponent } from './pages/PagesOption/PagesOption.component';
import { MasterUserComponent } from './layout/MasterUser/MasterUser.component';
import { MasterhomeComponent } from './pages/masterhome/masterhome.component';
import { PagesHomeCenterComponent } from './pages/PagesHomeCenter/PagesHomeCenter.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { ListAppComponent } from './pages/ListApp/ListApp.component';
const DEFAULT_ROUTES: Routes = [

  { path: 'warehouse-limit', component: WareHouseLimitComponent, data: { state: 'wh-limit' }, canActivate: [AuthGuard] },
  { path: 'warehouse-book', component: WareHouseBookComponent, data: { state: 'wh-book' } , canActivate: [AuthGuard]},
  { path: 'warehouse-benging', component: WareHouseBenginingComponent, data: { state: 'wh-benging' } , canActivate: [AuthGuard]},
  { path: 'warehouse-item-category', component: WareHouseItemCategoryComponent, data: { state: 'wh-item-ca' }, canActivate: [AuthGuard] },
  { path: 'warehouse-item', component: WareHouseItemComponent, data: { state: 'wh-item' } , canActivate: [AuthGuard]},
  { path: 'vendor', component: VendorComponent, data: { state: 'vender' } , canActivate: [AuthGuard]},
  { path: 'warehouse', component: WareHouseComponent, data: { state: 'warehouse' }, canActivate: [AuthGuard] },
  { path: 'unit', component: UnitComponent, data: { state: 'unit' } , canActivate: [AuthGuard]},
  { path: 'create-inward/:whid', component: InwardCreateComponent, data: { state: 'create-inward' } , canActivate: [AuthGuard]},
  { path: 'edit-inward/:id', component: InwardEditComponent, data: { state: 'edit-inward' } , canActivate: [AuthGuard]},
  { path: 'edit-outward/:id', component: OutwardEditComponent, data: { state: 'edit-outward' }, canActivate: [AuthGuard] },
  { path: 'details-inward/:id', component: InwardDetailsComponent, data: { state: 'details-inward' } , canActivate: [AuthGuard]},
  { path: 'details-outward/:id', component: OutwardDetailsComponent, data: { state: 'details-outward' } , canActivate: [AuthGuard]},
  { path: 'create-outward/:whid', component: OutwardCreateComponent, data: { state: 'create-outward' }, canActivate: [AuthGuard] },
  { path: 'report-total', component: ReportTotalComponent, data: { state: 'report-total' } , canActivate: [AuthGuard]},
  { path: 'report-details', component: ReportDetalisComponent, data: { state: 'report-details' }, canActivate: [AuthGuard] },
  { path: '', component: HomeComponent, data: { state: 'home' } , canActivate: [AuthGuard]},
  // { path: 'role', component: RoleUserComponent, data: { state: 'role' }, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
]

export const Authozire_LAYOUT: Routes = [
  { path: 'login', component: LoginComponent, data: { state: 'login' } },
  { path: 'registration', component: RegistrationComponent, data: { state: 'registration' } },

]

export const Master_LAYOUT: Routes = [
  { path: 'role', component: RoleUserComponent, data: { state: 'role', canActivate: [AuthGuard] } },
  { path: 'app', component: ListAppComponent, data: { state: 'app', canActivate: [AuthGuard] } },
  { path: '', component: MasterhomeComponent, data: { state: '', canActivate: [AuthGuard] } },
]
const routes: Routes = [

  { path: 'wh', component: DefaultLayoutComponent, children: DEFAULT_ROUTES, canActivate: [AuthGuard]   },
  { path: 'authozire', component: AuthozireComponent, children: Authozire_LAYOUT},
  { path: 'master', component: MasterUserComponent, children: Master_LAYOUT, canActivate: [AuthGuard] },
  { path: '404', component: NotFoundComponent, data: { state: '404' } },
  { path: '403', component: PagesForbieComponent, data: { state: '403' } },
  { path: 'page', component: PagesOptionComponent, data: { state: 'page' }, canActivate: [AuthGuard]},
  { path: 'center', component: PagesHomeCenterComponent, data: { state: 'center' },pathMatch: 'full'},
  { path: '', redirectTo: '/wh', pathMatch: 'full' },
  { path: '**', redirectTo: '/404' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
exports: [RouterModule]
})
export class AppRoutingModule { }
