import { BoatStatusType } from './boatStatusType';
import { BoatType } from './boatType';

export class BoatInformation{
    public id:number;
    public boatType: BoatType;
    public boatStatus : number;
    public boatSpeed : string;
    public boatReachTimeDuration:number;
    public boatActualTimeDuration : number;
}