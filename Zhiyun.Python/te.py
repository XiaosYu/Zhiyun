import torch

from zhiyun.modules import Module
from zhiyun.modules import __node_types__


class nGYyHE(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageOutput_zzpEXl = __node_types__['ImageOutput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.ReLU_7uX3Cs = __node_types__['ReLU']()
        self.BatchNormalization2D_D7a7wD = __node_types__['BatchNormalization2D'](FeatureNumber=3)
        self.ImageInput_FY7tpc = __node_types__['ImageInput'](ChannelNumber=16, ImageWidth=64, ImageHeight=64)
        self.Convolution2D_yVfIIO = __node_types__['Convolution2D'](InChannels=16, OutChannels=3, KernelWidth=3,
                                                                    KernelHeight=3, Stride=1, Padding=1, Bias=True)

    def forward(self, data):
        data_290022 = self.ImageInput_FY7tpc.forward(data)
        data_769102 = self.Convolution2D_yVfIIO.forward(data_290022)
        data_541167 = self.BatchNormalization2D_D7a7wD.forward(data_769102)
        data_593856 = self.ReLU_7uX3Cs.forward(data_541167)
        data_685114 = self.ImageOutput_zzpEXl.forward(data_593856)
        return data_685114


class vcfcsc(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageOutput_zzpEXl = __node_types__['ImageOutput'](ChannelNumber=16, ImageWidth=64, ImageHeight=64)
        self.ReLU_7uX3Cs = __node_types__['ReLU']()
        self.BatchNormalization2D_D7a7wD = __node_types__['BatchNormalization2D'](FeatureNumber=16)
        self.ImageInput_FY7tpc = __node_types__['ImageInput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.Convolution2D_yVfIIO = __node_types__['Convolution2D'](InChannels=3, OutChannels=16, KernelWidth=3,
                                                                    KernelHeight=3, Stride=1, Padding=1, Bias=True)

    def forward(self, data):
        data_290022 = self.ImageInput_FY7tpc.forward(data)
        data_769102 = self.Convolution2D_yVfIIO.forward(data_290022)
        data_541167 = self.BatchNormalization2D_D7a7wD.forward(data_769102)
        data_593856 = self.ReLU_7uX3Cs.forward(data_541167)
        data_685114 = self.ImageOutput_zzpEXl.forward(data_593856)
        return data_685114


class ceujee(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageInput_JHjP2e = __node_types__['ImageInput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.Addition_lwfTal = __node_types__['Addition']()
        self.ImageOutput_RaZ405 = __node_types__['ImageOutput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.标准卷积块_ml0dO4 = __node_types__['nGYyHE'](dTISlR=16, amqqYO=3, jbuVOf=3, buIw2b=3, e6OpJJ=1, iVSPYF=1,
                                                          zBpFSl=True)
        self.标准卷积块_gRFNc4 = __node_types__['vcfcsc'](dTISlR=3, amqqYO=16, jbuVOf=3, buIw2b=3, e6OpJJ=1, iVSPYF=1,
                                                          zBpFSl=True)

    def forward(self, data):
        data_884651 = self.ImageInput_JHjP2e.forward(data)
        data_949662 = self.标准卷积块_gRFNc4.forward(data_884651)
        data_748600 = self.标准卷积块_ml0dO4.forward(data_949662)
        data_307575 = self.Addition_lwfTal.forward(data_884651, data_748600)
        data_618963 = self.ImageOutput_RaZ405.forward(data_307575)
        return data_618963


class uwNT28(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageOutput_zzpEXl = __node_types__['ImageOutput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.ReLU_7uX3Cs = __node_types__['ReLU']()
        self.BatchNormalization2D_D7a7wD = __node_types__['BatchNormalization2D'](FeatureNumber=3)
        self.ImageInput_FY7tpc = __node_types__['ImageInput'](ChannelNumber=16, ImageWidth=64, ImageHeight=64)
        self.Convolution2D_yVfIIO = __node_types__['Convolution2D'](InChannels=16, OutChannels=3, KernelWidth=3,
                                                                    KernelHeight=3, Stride=1, Padding=1, Bias=True)

    def forward(self, data):
        data_290022 = self.ImageInput_FY7tpc.forward(data)
        data_769102 = self.Convolution2D_yVfIIO.forward(data_290022)
        data_541167 = self.BatchNormalization2D_D7a7wD.forward(data_769102)
        data_593856 = self.ReLU_7uX3Cs.forward(data_541167)
        data_685114 = self.ImageOutput_zzpEXl.forward(data_593856)
        return data_685114


class xlmTRo(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageOutput_zzpEXl = __node_types__['ImageOutput'](ChannelNumber=16, ImageWidth=64, ImageHeight=64)
        self.ReLU_7uX3Cs = __node_types__['ReLU']()
        self.BatchNormalization2D_D7a7wD = __node_types__['BatchNormalization2D'](FeatureNumber=16)
        self.ImageInput_FY7tpc = __node_types__['ImageInput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.Convolution2D_yVfIIO = __node_types__['Convolution2D'](InChannels=3, OutChannels=16, KernelWidth=3,
                                                                    KernelHeight=3, Stride=1, Padding=1, Bias=True)

    def forward(self, data):
        data_290022 = self.ImageInput_FY7tpc.forward(data)
        data_769102 = self.Convolution2D_yVfIIO.forward(data_290022)
        data_541167 = self.BatchNormalization2D_D7a7wD.forward(data_769102)
        data_593856 = self.ReLU_7uX3Cs.forward(data_541167)
        data_685114 = self.ImageOutput_zzpEXl.forward(data_593856)
        return data_685114


class kgmEEG(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageInput_JHjP2e = __node_types__['ImageInput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.Addition_lwfTal = __node_types__['Addition']()
        self.ImageOutput_RaZ405 = __node_types__['ImageOutput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.标准卷积块_ml0dO4 = __node_types__['uwNT28'](dTISlR=16, amqqYO=3, jbuVOf=3, buIw2b=3, e6OpJJ=1, iVSPYF=1,
                                                          zBpFSl=True)
        self.标准卷积块_gRFNc4 = __node_types__['xlmTRo'](dTISlR=3, amqqYO=16, jbuVOf=3, buIw2b=3, e6OpJJ=1, iVSPYF=1,
                                                          zBpFSl=True)

    def forward(self, data):
        data_884651 = self.ImageInput_JHjP2e.forward(data)
        data_949662 = self.标准卷积块_gRFNc4.forward(data_884651)
        data_748600 = self.标准卷积块_ml0dO4.forward(data_949662)
        data_307575 = self.Addition_lwfTal.forward(data_884651, data_748600)
        data_618963 = self.ImageOutput_RaZ405.forward(data_307575)
        return data_618963


class nDGV43(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageOutput_zzpEXl = __node_types__['ImageOutput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.ReLU_7uX3Cs = __node_types__['ReLU']()
        self.BatchNormalization2D_D7a7wD = __node_types__['BatchNormalization2D'](FeatureNumber=3)
        self.ImageInput_FY7tpc = __node_types__['ImageInput'](ChannelNumber=16, ImageWidth=64, ImageHeight=64)
        self.Convolution2D_yVfIIO = __node_types__['Convolution2D'](InChannels=16, OutChannels=3, KernelWidth=3,
                                                                    KernelHeight=3, Stride=1, Padding=1, Bias=True)

    def forward(self, data):
        data_290022 = self.ImageInput_FY7tpc.forward(data)
        data_769102 = self.Convolution2D_yVfIIO.forward(data_290022)
        data_541167 = self.BatchNormalization2D_D7a7wD.forward(data_769102)
        data_593856 = self.ReLU_7uX3Cs.forward(data_541167)
        data_685114 = self.ImageOutput_zzpEXl.forward(data_593856)
        return data_685114


class tyeB2g(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageOutput_zzpEXl = __node_types__['ImageOutput'](ChannelNumber=16, ImageWidth=64, ImageHeight=64)
        self.ReLU_7uX3Cs = __node_types__['ReLU']()
        self.BatchNormalization2D_D7a7wD = __node_types__['BatchNormalization2D'](FeatureNumber=16)
        self.ImageInput_FY7tpc = __node_types__['ImageInput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.Convolution2D_yVfIIO = __node_types__['Convolution2D'](InChannels=3, OutChannels=16, KernelWidth=3,
                                                                    KernelHeight=3, Stride=1, Padding=1, Bias=True)

    def forward(self, data):
        data_290022 = self.ImageInput_FY7tpc.forward(data)
        data_769102 = self.Convolution2D_yVfIIO.forward(data_290022)
        data_541167 = self.BatchNormalization2D_D7a7wD.forward(data_769102)
        data_593856 = self.ReLU_7uX3Cs.forward(data_541167)
        data_685114 = self.ImageOutput_zzpEXl.forward(data_593856)
        return data_685114


class xqkKuM(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageInput_JHjP2e = __node_types__['ImageInput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.Addition_lwfTal = __node_types__['Addition']()
        self.ImageOutput_RaZ405 = __node_types__['ImageOutput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.标准卷积块_ml0dO4 = __node_types__['nDGV43'](dTISlR=16, amqqYO=3, jbuVOf=3, buIw2b=3, e6OpJJ=1, iVSPYF=1,
                                                          zBpFSl=True)
        self.标准卷积块_gRFNc4 = __node_types__['tyeB2g'](dTISlR=3, amqqYO=16, jbuVOf=3, buIw2b=3, e6OpJJ=1, iVSPYF=1,
                                                          zBpFSl=True)

    def forward(self, data):
        data_884651 = self.ImageInput_JHjP2e.forward(data)
        data_949662 = self.标准卷积块_gRFNc4.forward(data_884651)
        data_748600 = self.标准卷积块_ml0dO4.forward(data_949662)
        data_307575 = self.Addition_lwfTal.forward(data_884651, data_748600)
        data_618963 = self.ImageOutput_RaZ405.forward(data_307575)
        return data_618963


class TestNet(Module):
    def __init__(self, *args, **kwargs):
        super(Module, self).__init__()
        self.ImageInput_Mz7jSC = __node_types__['ImageInput'](ChannelNumber=3, ImageWidth=64, ImageHeight=64)
        self.Basic残差块_jsdg40 = __node_types__['ceujee'](rZaelY=16, kQZoxa=3, r776qv=3)
        self.Addition_2KVJKy = __node_types__['Addition']()
        self.Basic残差块_2fSY0E = __node_types__['kgmEEG'](rZaelY=16, kQZoxa=3, r776qv=3)
        self.Addition_WyZoou = __node_types__['Addition']()
        self.Addition_pElapI = __node_types__['Addition']()
        self.Flatten_cJ19bu = __node_types__['Flatten']()
        self.ReLU_tCTHhk = __node_types__['ReLU']()
        self.VectorOutput_C6RAto = __node_types__['VectorOutput'](OutNumber=10)
        self.Basic残差块_D8N2tW = __node_types__['xqkKuM'](rZaelY=16, kQZoxa=3, r776qv=3)
        self.Flatten_2JIplE = __node_types__['Flatten']()
        self.Concatenate_jNo3Br = __node_types__['Concatenate'](ConnectedDim=1)
        self.MiddleMlpLayer = __node_types__['Linear'](InFeatures=192, OutFeatures=192, Bias=True)
        self.Classifier = __node_types__['Linear'](InFeatures=384, OutFeatures=10, Bias=True)

    def forward(self, data):
        data_342768 = self.ImageInput_Mz7jSC.forward(data)
        data_507975 = self.Basic残差块_D8N2tW.forward(data_342768)
        data_823861 = self.Addition_WyZoou.forward(data_342768, data_507975)
        data_438157 = self.Basic残差块_jsdg40.forward(data_823861)
        data_875711 = self.Addition_2KVJKy.forward(data_438157, data_823861)
        data_131183 = self.Basic残差块_2fSY0E.forward(data_875711)
        data_293704 = self.Addition_pElapI.forward(data_875711, data_131183)
        data_970365 = self.Flatten_cJ19bu.forward(data_293704)
        data_939381 = self.Flatten_2JIplE.forward(data_342768)
        data_284095 = self.MiddleMlpLayer.forward(data_939381)
        data_593504 = self.ReLU_tCTHhk.forward(data_284095)
        data_222028 = self.Concatenate_jNo3Br.forward(data_970365, data_593504)
        data_033301 = self.Classifier.forward(data_222028)
        data_828395 = self.VectorOutput_C6RAto.forward(data_033301)
        return data_828395


__node_types__['ceujee'] = ceujee
__node_types__['nGYyHE'] = nGYyHE
__node_types__['vcfcsc'] = vcfcsc
__node_types__['kgmEEG'] = kgmEEG
__node_types__['uwNT28'] = uwNT28
__node_types__['xlmTRo'] = xlmTRo
__node_types__['xqkKuM'] = xqkKuM
__node_types__['nDGV43'] = nDGV43
__node_types__['tyeB2g'] = tyeB2g

model = TestNet()
out = model(torch.rand(7, 3, 64, 64))
