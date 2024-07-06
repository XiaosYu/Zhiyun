import argparse


def set_args():
    parser = argparse.ArgumentParser()

    parser.add_argument('--config', default='train.json', type=str, required=False, help='训练文件位置')

    args = parser.parse_args()
    return args


