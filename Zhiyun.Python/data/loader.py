from pandas import read_csv
from numpy import ndarray, array
from PIL.Image import open, ROTATE_90
from os import listdir


def load_sequences(parameters) -> (ndarray, ndarray):
    """
    从csv文件里面获取序列数据
    :param parameters: 参数字典
    :return: (特征序列, 目标序列)
    """
    rp = parameters['ReadParameters']
    data = read_csv(**rp).to_numpy()
    features = parameters['Features']
    targets = parameters['Targets']
    return data[:, features], data[:, targets]


def load_images(parameters) -> (ndarray, ndarray):
    """
        从文件夹分类里面获取图像数据
        :param parameters: 参数字典
        :return: (图像集合, 类别集合)
        """
    dir_path = parameters['Path']
    # 获取图片文件夹
    dirs = listdir(dir_path)
    # 获取图片集
    images, category = [], []
    # 获取图片大小
    size = tuple(parameters['Size'])
    for (idx, name) in zip(range(len(dirs)), dirs):
        for path in listdir(f'{dir_path}/{name}'):
            path = f'{dir_path}/{name}/{path}'
            image = open(path)
            image = image.resize(size)
            image = image.transpose(ROTATE_90)
            image = array(image).T
            images.append(image)
            category.append(idx)
    return array(images).astype('float'), array(category).reshape(-1, 1).astype('long')


images, category = load_images({
    'Path': '../images',
    'Size': [1024, 1024]
})
print(images.shape, category.shape)
