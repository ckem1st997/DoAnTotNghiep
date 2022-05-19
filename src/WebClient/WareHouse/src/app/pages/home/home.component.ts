import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Unit } from 'src/app/entity/Unit';
import { DashBoardSelectTopInAndOutDTO } from 'src/app/model/DashBoardSelectTopInAndOutDTO';
import { ResultDataResponse } from 'src/app/model/ResultDataResponse';
import { ResultMessageResponse } from 'src/app/model/ResultMessageResponse';
import { SelectTopDashBoardDTO } from 'src/app/model/SelectTopDashBoardDTO';
import { DashBoardService } from 'src/app/service/DashBoard.service';
import { UnitValidator } from 'src/app/validator/UnitValidator';
import { WareHouse } from 'src/app/entity/WareHouse';
import { WareHouseBookService } from 'src/app/service/WareHouseBook.service';
import { WareHouseBookDTO } from './../../model/WareHouseBookDTO';
import { DashBoardChartInAndOutCountDTO } from 'src/app/model/DashBoardChartInAndOutCountDTO';
import { SelectTopWareHouseDTO } from 'src/app/model/SelectTopWareHouseDTO';
import { NotifierService } from 'angular-notifier';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/extension/Authentication.service';
import { SignalRService } from 'src/app/service/SignalR.service';


interface ChartPhieu {
  value: boolean;
  viewValue: string;
}

interface ChartPhieu2 {
  value: number;
  viewValue: string;
}
interface DataChart {
  value: string;
  name: string;
}


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit,OnDestroy {

  //
  private readonly notifier!: NotifierService;
  typeIn = "Phiếu nhập";
  typeOut = "Phiếu xuất";
  //
  checkChart: boolean = true;
  checkChart2: number = 0;
  dataChart: DataChart[] = [];
  dataChart2: DataChart[] = [];
  getDashBoardWareHouse: ResultMessageResponse<SelectTopWareHouseDTO> = {
    success: false,
    code: '',
    httpStatusCode: 0,
    title: '',
    message: '',
    data: [],
    totalCount: 0,
    isRedirect: false,
    redirectUrl: '',
    errors: {}
  };

  getDashBoardByYear: DashBoardChartInAndOutCountDTO = {
    inward: null,
    outward: null
  }

  // lisst history

  listHistory: ResultMessageResponse<WareHouseBookDTO> = {
    success: false,
    code: '',
    httpStatusCode: 0,
    title: '',
    message: '',
    data: [],
    totalCount: 0,
    isRedirect: false,
    redirectUrl: '',
    errors: {}
  };;


  //list in and out

  listIn: ResultMessageResponse<DashBoardSelectTopInAndOutDTO> = {
    success: false,
    code: '',
    httpStatusCode: 0,
    title: '',
    message: '',
    data: [],
    totalCount: 0,
    isRedirect: false,
    redirectUrl: '',
    errors: {}
  };


  listOut: ResultMessageResponse<DashBoardSelectTopInAndOutDTO> = {
    success: false,
    code: '',
    httpStatusCode: 0,
    title: '',
    message: '',
    data: [],
    totalCount: 0,
    isRedirect: false,
    redirectUrl: '',
    errors: {}
  };

  listIndex: SelectTopDashBoardDTO = {
    itemCountMax: undefined,
    itemCountMin: undefined,
    wareHouseBeginningCountMax: undefined,
    wareHouseBeginningCountMin: undefined
  };

  //
  optioneChartCoulumn: any;
  optioneChart2: any;

  gaugeData = [
    {
      value: 20,
      name: 'Perfect',
      title: {
        offsetCenter: ['0%', '-30%']
      },
      detail: {
        valueAnimation: true,
        offsetCenter: ['0%', '-20%']
      }
    },
    {
      value: 40,
      name: 'Good',
      title: {
        offsetCenter: ['0%', '0%']
      },
      detail: {
        valueAnimation: true,
        offsetCenter: ['0%', '10%']
      }
    },
    {
      value: 60,
      name: 'Commonly',
      title: {
        offsetCenter: ['0%', '30%']
      },
      detail: {
        valueAnimation: true,
        offsetCenter: ['0%', '40%']
      }
    }
  ];
  colors = ['#5470C6', '#91CC75', '#EE6666'];

  selectedValue: string | undefined;
  selectedCar: string | undefined;

  foods: ChartPhieu[] = [
    { value: true, viewValue: 'Phiếu nhập' },
    { value: false, viewValue: 'Phiếu xuất' }
  ];

  foods2: ChartPhieu2[] = [
    { value: 1, viewValue: 'Tháng 1' },
    { value: 2, viewValue: 'Tháng 2' },
    { value: 3, viewValue: 'Tháng 3' },
    { value: 4, viewValue: 'Tháng 4' },
    { value: 5, viewValue: 'Tháng 5' },
    { value: 6, viewValue: 'Tháng 6' },
    { value: 7, viewValue: 'Tháng 7' },
    { value: 8, viewValue: 'Tháng 8' },
    { value: 9, viewValue: 'Tháng 9' },
    { value: 10, viewValue: 'Tháng 10' },
    { value: 11, viewValue: 'Tháng 11' },
    { value: 12, viewValue: 'Tháng 12' }
  ];
  constructor(public signalRService: SignalRService, private route: Router, private dashboard: DashBoardService, private warehouseBook: WareHouseBookService, notifierService: NotifierService) {
    this.notifier = notifierService;

  }
  @HostListener('window:resize', ['$event'])

  SetHeightDashboard() {
    const getScreenHeight = window.innerHeight;
    const table = document.getElementById("dashboard") as HTMLDivElement;
    table.style.height = getScreenHeight - 95 + "px";
  }

  onWindowResize(): void {
    this.SetHeightDashboard();


  }

  ngOnInit(): void {


    // lắng nghe khi signalR có sự thay đổi
    this.signalRService.hubConnection.on(this.signalRService.WareHouseBookTrachkingToCLient, (data: ResultMessageResponse<string>) => {
      if (data.success) {
        this.notifier.notify('success', data.message);
        this.GetData();
      }
    });
    this.signalRService.hubConnection.on(this.signalRService.CreateWareHouseBookTrachking, (data: ResultMessageResponse<string>) => {
      if (data.success) {
        this.notifier.notify('success', data.message);
        this.GetData();
      }
    });
    this.signalRService.hubConnection.on(this.signalRService.DeleteWareHouseBookTrachking, (data: ResultMessageResponse<string>) => {
      if (data.success) {
        this.notifier.notify('success', data.message);
        this.GetData();
      }
    });
    this.GetData();
  }

  ngOnDestroy(): void {
    // tắt phương thức vừa gọi để tránh bị gọi lại nhiều lần
    this.signalRService.hubConnection.off(this.signalRService.WareHouseBookTrachkingToCLient);
    this.signalRService.hubConnection.off(this.signalRService.CreateWareHouseBookTrachking);
    this.signalRService.hubConnection.off(this.signalRService.DeleteWareHouseBookTrachking);

  }

  GetData() {
    this.SetHeightDashboard();
    this.getIn();
    this.getOut();
    this.getIndex();
    this.getHistory();
    this.getChartToYear(2021);
    this.getChartToWarehouse();
  }
  getIn() {
    this.dashboard.getTopInward().subscribe(
      (data) => {
        this.listIn = data;
      }
    );
  }
  getOut() {
    this.dashboard.getTopOutward().subscribe(
      (data) => {
        this.listOut = data;
      }
    );
  }
  getIndex() {

    this.dashboard.getTopIndex().subscribe(
      (data) => {
        this.listIndex = data.data;
      }
    );
  }


  getHistory() {
    this.dashboard.getHistory().subscribe(
      (data) => {
        this.listHistory = data;
      }
    );
  }

  getDateToName(d: Date) {
    return d.toString().replace('T', '-');
  }

  changeChartPhieu(e: any) {
    this.getChartToYear(2021);
  }
  changeChartPhieu2(e: any) {
    this.getChartToMouth();

  }
  getChartToMouth() {
    var d = new Date().getFullYear();
    this.dataChart2 = [];
    if (this.checkChart2 > 0)
      this.dashboard.getChartByMouth(d + '-' + this.checkChart2 + '-1', d + '-' + this.checkChart2 + '-' + this.daysInMonth(this.checkChart2, d)).subscribe(
        (data) => {
          if (this.checkChart) {
            if (data.data.inward !== undefined && data.data.inward !== null) {
              for (let index = 0; index < data.data.inward.length; index++) {
                const element = data.data.inward[index];
                this.dataChart2.push({
                  name: element.name,
                  value: element.count.toString()
                });
              }
            }
          }
          else {
            if (data.data.outward !== undefined && data.data.outward !== null) {
              for (let index = 0; index < data.data.outward.length; index++) {
                const element = data.data.outward[index];
                this.dataChart2.push({
                  name: element.name,
                  value: element.count.toString()
                });
              }
            }
          }

          this.optioneChart2 = {
            title: {
              text: 'Biểu đồ phiếu',
              subtext: 'Năm ' + d + '',
              left: 'center',
              type: 'pie',
              selectedMode: 'single',
              radius: ['40%', '70%'],
            },
            tooltip: {
              trigger: 'item'
            },
            legend: {
              orient: 'vertical',
              left: 'left'
            },
            series: [
              {
                name: 'Số lượng phiếu',
                type: 'pie',
                radius: '50%',
                data: this.dataChart2,
                emphasis: {
                  itemStyle: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                  }
                }
              }
            ]
          };

        }
      );
  }

  getChartToYear(d: number) {
    // if (d === undefined || d < 1900)
    // d = new Date().getFullYear();
    this.dataChart2 = [];
    this.dashboard.getChartByYear(2022).subscribe(
      (data) => {
        if (this.checkChart) {
          if (data.data.inward !== undefined && data.data.inward !== null) {
            for (let index = 0; index < data.data.inward.length; index++) {
              const element = data.data.inward[index];
              this.dataChart2.push({
                name: element.name,
                value: element.count.toString()
              });
            }
          }
        }
        else {
          if (data.data.outward !== undefined && data.data.outward !== null) {
            for (let index = 0; index < data.data.outward.length; index++) {
              const element = data.data.outward[index];
              this.dataChart2.push({
                name: element.name,
                value: element.count.toString()
              });
            }
          }
        }

        this.optioneChart2 = {
          title: {
            text: 'Biểu đồ phiếu',
            subtext: 'Năm ' + d + '',
            left: 'center',
            type: 'pie',
            selectedMode: 'single',
            radius: ['40%', '70%'],
          },
          tooltip: {
            trigger: 'item'
          },
          legend: {
            orient: 'vertical',
            left: 'left'
          },
          series: [
            {
              name: 'Số lượng phiếu',
              type: 'pie',
              radius: '50%',
              data: this.dataChart2,
              emphasis: {
                itemStyle: {
                  shadowBlur: 10,
                  shadowOffsetX: 0,
                  shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
              }
            }
          ]
        };

      }
    );
  }

  getChartToWarehouse() {
    this.dataChart= [];
    this.dashboard.getChartByWareHouse().subscribe(
      (data) => {
        if (data.data !== undefined && data.data !== null) {
          for (let index = 0; index < data.data.length; index++) {
            const element = data.data[index];
            this.dataChart.push({
              name: element.name.replace('Kho', ''),
              value: element.sumBalance
            });
          }
        }
        this.optioneChartCoulumn = {
          title: {
            text: 'Biểu đồ tồn kho',
            subtext: 'Năm 2022',
            left: 'center'
          },
          tooltip: {
            trigger: 'item'
          },
          legend: {
            orient: 'vertical',
            left: 'left'
          },
          series: [
            {
              name: 'Số lượng tồn',
              type: 'pie',
              radius: '50%',
              data: this.dataChart,
              emphasis: {
                itemStyle: {
                  shadowBlur: 10,
                  shadowOffsetX: 0,
                  shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
              }
            }
          ]
        };

      }
    );
  }
  onChartInit(ec: any) {
    console.log('onChartInit', ec);
  }

  openDialog(model: WareHouseBookDTO) {
    if (model.id !== null) {
      if (model.type === this.typeIn)
        this.route.navigate(['wh/details-inward', model.id]);
      else if (model.type === this.typeOut)
        this.route.navigate(['wh/details-outward', model.id]);
    }
    else
      this.notifier.notify('warning', "Xin vui lòng thử lại !");
  }
  ConvertStringToNumber(input: string) {
    var numeric = Number(input);
    return numeric;
  }
  daysInMonth(month: number, year: number) {
    return new Date(year, month, 0).getDate();
  }
}
function moment(arg0: string, arg1: string) {
  throw new Error('Function not implemented.');
}

