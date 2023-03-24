import { UserLevel } from '@/constants';
import {
  BetSettingGroupTypeIds,
  CommissionTypeIds,
  ParlayMaxPayoutTypeIds,
  PositionTakingIds,
} from '@/constants/product';
import { GroupItem, GroupTypeDict, ParlayMaxPayoutItem, ProductGroupDict } from '@/types/new-account';
import { getNumberOfCommaString } from '@/utils/number-format';

import getBetSettingHandler from './betSettingHandler';
import getPTSettingHandler from './ptSettingHandler';

type SettingOptions = {
  userLevel: UserLevel;
  commissions: GroupTypeDict;
  betSettings: GroupTypeDict;
  parlayMaxPayouts: GroupTypeDict;
  ptSettings: { [key: number]: [] };
};

export const resetName = (groupItem: GroupItem) => {
  return {
    ...groupItem,
    betTypeName: '',
    sportTypeName: '',
    displayName: '',
    subGroupName: '',
  };
};

export default class BaseSettingHandler {
  protected productGroups: ProductGroupDict;
  protected settings: SettingOptions;

  constructor(productGroups: ProductGroupDict, settings: SettingOptions) {
    this.productGroups = productGroups;
    this.settings = settings;
  }

  protected getGroupTypeIndex(productGroupId: number, groupTypeId: number) {
    const productGroup = this.productGroups[productGroupId];
    return productGroup ? productGroup.groupTypes.findIndex((groupType) => groupType.id === groupTypeId) : -1;
  }

  private commissionSettings() {
    Object.entries(CommissionTypeIds).forEach((commissionId) => {
      const [productId, groupTypeId] = [Number(commissionId[0]), Number(commissionId[1])];
      const groupTypeIndex = this.getGroupTypeIndex(productId, groupTypeId);

      if (groupTypeIndex < 0) return;
      this.productGroups[productId].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].groupItems = this.settings.commissions[
        productId
      ].groupItems.map((item) => ({
        ...item,
        ...resetName(item),
      }));
    });

    return this;
  }

  private betSettings() {
    Object.entries(BetSettingGroupTypeIds).forEach((betSettingId) => {
      const [productId, groupTypeId] = [Number(betSettingId[0]), Number(betSettingId[1])];
      const groupTypeIndex = this.getGroupTypeIndex(productId, groupTypeId);

      if (groupTypeIndex < 0) return;
      const groupType = this.settings.betSettings[groupTypeId];
      const handler = getBetSettingHandler(groupType);
      if (!handler) return;
      this.productGroups[productId].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].groupItems = handler.handle(groupType);
    });

    return this;
  }

  private parlayMaxPayoutSettings() {
    Object.entries(ParlayMaxPayoutTypeIds).forEach((parlayId) => {
      const [productId, groupTypeId] = [Number(parlayId[0]), Number(parlayId[1])];
      const groupTypeIndex = this.getGroupTypeIndex(productId, groupTypeId);

      if (groupTypeIndex < 0) return;
      const groupItems = this.settings.parlayMaxPayouts[groupTypeId].groupItems as ParlayMaxPayoutItem[];
      this.productGroups[productId].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].groupItems = groupItems.map((item) => ({
        ...item,
        ...resetName(item),
        singleMatchParlay: getNumberOfCommaString(item.singleMatchParlay),
        combo2: getNumberOfCommaString(item.combo2),
        combo35: getNumberOfCommaString(item.combo35),
        comboOver6: getNumberOfCommaString(item.comboOver6),
      }));
    });

    return this;
  }

  private ptSettings() {
    Object.entries(PositionTakingIds).forEach((parlayId) => {
      const [productId, groupTypeId] = [Number(parlayId[0]), Number(parlayId[1])];
      const groupTypeIndex = this.getGroupTypeIndex(productId, groupTypeId);

      if (groupTypeIndex < 0) return;
      const ptConfigs = this.productGroups[productId].groupTypes[groupTypeIndex].groupItems;
      const ptSettings = this.settings.ptSettings[productId];
      const handler = getPTSettingHandler(this.settings.userLevel);
      if (!handler) return;
      this.productGroups[productId].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].name = '';
      this.productGroups[productId].groupTypes[groupTypeIndex].groupItems = handler.handle(ptConfigs, ptSettings);
    });

    return this;
  }

  getProductGroupSettings() {
    this.commissionSettings().betSettings().parlayMaxPayoutSettings().ptSettings();
    return this.productGroups;
  }
}
