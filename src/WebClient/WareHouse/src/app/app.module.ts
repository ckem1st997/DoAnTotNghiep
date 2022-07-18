import { HomeComponent } from './pages/home/home.component';
import { WareHouseLimitComponent } from './pages/WareHouseLimit/WareHouseLimit.component';
import { WareHouseBookComponent } from './pages/WareHouseBook/WareHouseBook.component';
import { WareHouseBenginingComponent } from './pages/WareHouseBengining/WareHouseBengining.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './layout/header/header.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { AngularSplitModule } from 'angular-split';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatInputModule } from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatTreeModule } from '@angular/material/tree';
import { VendorService } from './service/VendorService.service';
import { VendorComponent } from './pages/Vendor/Vendor.component';
import { CdkTreeModule } from '@angular/cdk/tree';
import { VendorEditComponent } from './method/edit/VendorEdit/VendorEdit.component';
import { NotifierModule, NotifierOptions } from 'angular-notifier';
import { VendorDetailsComponent } from './method/details/VendorDetails/VendorDetails.component';
import { VendorCreateComponent } from './method/create/VendorCreate/VendorCreate.component';
import { VendorDeleteComponent } from './method/delete/VendorDelete/VendorDelete.component';
import { FormsearchComponent } from './method/search/formsearch/formsearch.component';
import { WareHouseComponent } from './pages/WareHouse/WareHouse.component';
import { WareHouseCreateComponent } from './method/create/WareHouseCreate/WareHouseCreate.component';
import { WareHouseEditComponent } from './method/edit/WareHouseEdit/WareHouseEdit.component';
import { WareHouseDetailsComponent } from './method/details/WareHouseDetails/WareHouseDetails.component';
import { WareHouseDeleteComponent } from './method/delete/WareHouseDelete/WareHouseDelete.component';
import { FormSearchWareHouseComponent } from './method/search/FormSearchWareHouse/FormSearchWareHouse.component';
import { UnitComponent } from './pages/Unit/Unit.component';
import { UnitCreateComponent } from './method/create/UnitCreate/UnitCreate.component';
import { UnitEditComponent } from './method/edit/UnitEdit/UnitEdit.component';
import { UnitDetailsComponent } from './method/details/UnitDetails/UnitDetails.component';
import { UnitDeleteComponent } from './method/delete/UnitDelete/UnitDelete.component';
import { WareHouseItemCategoryComponent } from './pages/WareHouseItemCategory/WareHouseItemCategory.component';
import { WareHouseItemCategoryCreateComponent } from './method/create/WareHouseItemCategoryCreate/WareHouseItemCategoryCreate.component';
import { WareHouseItemCategoryEditComponent } from './method/edit/WareHouseItemCategoryEdit/WareHouseItemCategoryEdit.component';
import { WareHouseItemCategoryDelelteComponent } from './method/delete/WareHouseItemCategoryDelelte/WareHouseItemCategoryDelelte.component';
import { WareHouseItemCategoryEditDetailsComponent } from './method/details/WareHouseItemCategoryEditDetails/WareHouseItemCategoryEditDetails.component';
import { WareHouseItemComponent } from './pages/WareHouseItem/WareHouseItem.component';
import { WareHouseItemCreateComponent } from './method/create/WareHouseItemCreate/WareHouseItemCreate.component';
import { WareHouseItemService } from './service/WareHouseItem.service';
import { WareHouseItemEditComponent } from './method/edit/WareHouseItemEdit/WareHouseItemEdit.component';
import { WareHouseItemDetailsComponent } from './method/details/WareHouseItemDetails/WareHouseItemDetails.component';
import { WareHouseItemDeleteComponent } from './method/delete/WareHouseItemDelete/WareHouseItemDelete.component';
import { WareHouseItemUnitDelelteComponent } from './method/delete/WareHouseItemUnitDelelte/WareHouseItemUnitDelelte.component';
import { WareHouseItemUnitCreateComponent } from './method/create/WareHouseItemUnitCreate/WareHouseItemUnitCreate.component';
import { WareHouseBenginingEditComponent } from './method/edit/WareHouseBenginingEdit/WareHouseBenginingEdit.component';
import { WareHouseBenginingCreateComponent } from './method/create/WareHouseBenginingCreate/WareHouseBenginingCreate.component';
import { WareHouseBenginingCreateDeleteComponent } from './method/delete/WareHouseBenginingCreateDelete/WareHouseBenginingCreateDelete.component';
import { WareHouseLimitCreateComponent } from './method/create/WareHouseLimitCreate/WareHouseLimitCreate.component';
import { WareHouseLimitEditComponent } from './method/edit/WareHouseLimitEdit/WareHouseLimitEdit.component';
import { WareHouseLimitDeleteComponent } from './method/delete/WareHouseLimitDelete/WareHouseLimitDelete.component';
import { FormSearchBeginningComponent } from './method/search/formSearchBeginning/formSearchBeginning.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule, MAT_DATE_LOCALE} from '@angular/material/core';
import { FormSearchWareHouseBookComponent } from './method/search/formSearchWareHouseBook/formSearchWareHouseBook.component';
import { InwardCreateComponent } from './method/create/InwardCreate/InwardCreate.component';
import { InwarDetailsCreateComponent } from './method/create/InwarDetailsCreate/InwarDetailsCreate.component';
import {MatChipsModule} from '@angular/material/chips';
import { InwarDetailsEditComponent } from './method/edit/InwarDetailsEdit/InwarDetailsEdit.component';
import { TagInputModule } from 'ngx-chips';
import { InwardEditComponent } from './method/edit/InwardEdit/InwardEdit.component';
import { OutwardCreateComponent } from './method/create/OutwardCreate/OutwardCreate.component';
import { OutwarDetailsCreateComponent } from './method/create/OutwarDetailsCreate/OutwarDetailsCreate.component';
import { OutwarDetailsEditComponent } from './method/edit/OutwarDetailsEdit/OutwarDetailsEdit.component';
import { InwarDetailsEditByServiceComponent } from './method/edit/InwarDetailsEditByService/InwarDetailsEditByService.component';
import { ErrorIntercept } from './extension/ErrorIntercept';
import { InwardDetailsComponent } from './method/details/InwardDetails/InwardDetails.component';
import { InwardDetailDetailsComponent } from './method/details/InwardDetailDetails/InwardDetailDetails.component';
import { OutwardDetailDetailsComponent } from './method/details/OutwardDetailDetails/OutwardDetailDetails.component';
import { OutwardDetailsComponent } from './method/details/OutwardDetails/OutwardDetails.component';
import { OutwardetailsEditByServiceComponent } from './method/edit/OutwardetailsEditByService/OutwardetailsEditByService.component';
import { OutwardEditComponent } from './method/edit/OutwardEdit/OutwardEdit.component';
import { WareHouseBookDeleteComponent } from './method/delete/WareHouseBookDelete/WareHouseBookDelete.component';
import { WareHouseBookDeleteAllComponent } from './method/delete/WareHouseBookDeleteAll/WareHouseBookDeleteAll.component';
import { ReportTotalComponent } from './pages/ReportTotal/ReportTotal.component';
import { FormSearchReportTotalComponent } from './method/search/FormSearchReportTotal/FormSearchReportTotal.component';
import { ReportDetalisComponent } from './pages/ReportDetalis/ReportDetalis.component';
import { FormSearchReportDetailsComponent } from './method/search/FormSearchReportDetails/FormSearchReportDetails.component';
import {MatSelectModule} from '@angular/material/select';
import { NgxEchartsModule } from 'ngx-echarts';
import { LoginComponent } from './pages/login/login.component';
import { RoleUserComponent } from './pages/RoleUser/RoleUser.component';
import { CountUpModule } from 'ngx-countup';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { DefaultLayoutComponent } from './layout/Default-Layout/Default-Layout.component';
import { AuthozireComponent } from './layout/Authozire/Authozire.component';
import { PagesForbieComponent } from './pages/PagesForbie/PagesForbie.component';
import { HeaderMasterComponent } from './layout/headerMaster/headerMaster.component';
import { PagesOptionComponent } from './pages/PagesOption/PagesOption.component';
import { MasterUserComponent } from './layout/MasterUser/MasterUser.component';
import { MasterhomeComponent } from './pages/masterhome/masterhome.component';
import { RoleEditComponent } from './method/edit/RoleEdit/RoleEdit.component';
import { SelectWareHouseComponent } from './method/edit/selectWareHouse/selectWareHouse.component';
import { PagesHomeCenterComponent } from './pages/PagesHomeCenter/PagesHomeCenter.component';
import { RegistrationComponent } from './pages/registration/registration.component';
import { SignalRService } from './service/SignalR.service';
import { HttpCancelService } from './extension/HttpCancel.service';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatSliderModule} from '@angular/material/slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { SpeedTestModule } from 'ng-speed-test';
const customNotifierOptions: NotifierOptions = {
  position: {
    horizontal: {
      position: 'middle',
      distance: 12
    },
    vertical: {
      position: 'bottom',
      distance: 100,
      gap: 10
    }
  },
  theme: 'material',
  behaviour: {
    autoHide: 2500,
    onClick: 'hide',
    onMouseover: 'pauseAutoHide',
    showDismissButton: true,
    stacking: 4
  },
  animations: {
    enabled: true,
    show: {
      preset: 'slide',
      speed: 300,
      easing: 'ease'
    },
    hide: {
      preset: 'fade',
      speed: 300,
      easing: 'ease',
      offset: 50
    },
    shift: {
      speed: 300,
      easing: 'ease'
    },
    overlap: 150
  }
};
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    WareHouseBenginingComponent,
    WareHouseBookComponent,
    WareHouseLimitComponent,
    HomeComponent,
    VendorComponent,
    VendorEditComponent,
    VendorDetailsComponent,
    VendorCreateComponent,
    VendorDeleteComponent,
    FormsearchComponent,
    WareHouseComponent,
    WareHouseCreateComponent,
    WareHouseEditComponent,
    WareHouseDetailsComponent,
    WareHouseDeleteComponent,
    FormSearchWareHouseComponent,
    UnitComponent,
    UnitCreateComponent,
    UnitEditComponent,
    UnitDetailsComponent,
    UnitDeleteComponent,
    WareHouseItemCategoryComponent,
    WareHouseItemCategoryCreateComponent,
    WareHouseItemCategoryEditComponent,
    WareHouseItemCategoryDelelteComponent,
    WareHouseItemCategoryEditDetailsComponent,
    WareHouseItemComponent,
    WareHouseItemCreateComponent,
    WareHouseItemEditComponent,
    WareHouseItemDetailsComponent,
    WareHouseItemDeleteComponent,
    WareHouseItemUnitDelelteComponent,
    WareHouseItemUnitCreateComponent,
    WareHouseBenginingComponent,
    WareHouseBenginingEditComponent,
    WareHouseBenginingCreateComponent,
    WareHouseBenginingCreateDeleteComponent,
    WareHouseLimitComponent,
    WareHouseLimitCreateComponent,
    WareHouseLimitEditComponent,
    WareHouseLimitDeleteComponent,
    WareHouseBookComponent,
    FormSearchBeginningComponent,
    FormSearchWareHouseBookComponent,
    InwardCreateComponent,
    InwarDetailsCreateComponent,
    InwarDetailsEditComponent,
    InwardEditComponent,
    OutwardCreateComponent,
    OutwarDetailsCreateComponent,
    OutwarDetailsEditComponent,
    InwarDetailsEditByServiceComponent,
    InwardDetailsComponent,
    InwardDetailDetailsComponent,
    OutwardDetailDetailsComponent,
    OutwardDetailsComponent,
    OutwardetailsEditByServiceComponent,
    OutwardEditComponent,
    WareHouseBookDeleteComponent,
    WareHouseBookDeleteAllComponent,
    ReportTotalComponent,
    FormSearchReportTotalComponent,
    ReportDetalisComponent,
    FormSearchReportDetailsComponent,
    LoginComponent,
    RoleUserComponent,
    DefaultLayoutComponent,
    AuthozireComponent,
    PagesForbieComponent,
    HeaderMasterComponent,
    PagesOptionComponent,
    MasterUserComponent,
    MasterhomeComponent,
    RoleEditComponent,
    SelectWareHouseComponent,
    PagesHomeCenterComponent,
    RegistrationComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatMenuModule,
    AngularSplitModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatCheckboxModule,
    FormsModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatButtonModule,
    ScrollingModule,
    MatInputModule,
    MatIconModule,
    MatSortModule,
    MatDialogModule,
    MatExpansionModule,
    MatGridListModule,
    AngularSplitModule,
    MatTreeModule,
    CdkTreeModule,
    NotifierModule.withConfig(customNotifierOptions),
    MatDatepickerModule,
    MatNativeDateModule,
    MatChipsModule,
    TagInputModule,
    CountUpModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    NgxEchartsModule.forRoot({
      echarts: () => import('echarts')
    }),
    MatSelectModule,
    SpeedTestModule,

  ],
  providers: [VendorService,WareHouseItemService,{ provide: MAT_DATE_LOCALE, useValue: 'en-GB' }, {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorIntercept,
    multi: true
  },HeaderComponent,SignalRService,HttpCancelService],
  bootstrap: [AppComponent],
})
export class AppModule { }
