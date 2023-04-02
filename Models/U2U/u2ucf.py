import numpy as np
import pandas as pd
from typing import List

def lmap(func, enumerable):
    return list(map(
        func,
        enumerable
    ))

def nmap(func, enumerable):
    return np.array(lmap(func, enumerable))

def pad_if_needed(array, target_length):
    delta = target_length - len(array)
    if delta<=0:
        return array
    return np.pad(array, (0, delta), mode='constant')

def apk(relevant: List[int], predicted: List[int], k: int = 20):
    rlen = len(relevant)
    if rlen==0:
        return 0
    
    mi_dict = {}
    rel_set = set(relevant)
    result = 0

    plen = min(len(predicted), k)
    for i in range(plen):
        element = predicted[i]
        prev_index = mi_dict.get(element)
        if prev_index is None:
            if element in rel_set:
                mi_dict[element] = i
                result = result + len(mi_dict)/(i + 1.0)
    
    return result / min(rlen, k)

def mapk(relevant: List[List[int]], predicted: List[List[int]], k: int = 20):
    return np.mean(nmap(
        lambda i: apk(relevant[i], predicted[i], k),
        range(len(relevant))
    ))

def similarity(ratings1: dict, ratings2: dict) -> float:
    keyset = set(ratings1.keys()).intersection(set(ratings2.keys()))
    if len(keyset) == 0:
        return 0
    corr_sum = sum(map(
        lambda key: ratings1[key]*ratings2[key],
        keyset
    ))
    norm1 = np.linalg.norm(list(ratings1.values()))
    norm2 = np.linalg.norm(list(ratings2.values()))
    if norm1 == 0 or norm2 == 0:
        return 0
    return corr_sum/(norm1*norm2)

def add_ratings_dict(main_dict: dict, dict2add: dict):
    for k, v in dict2add.items():
        newValue = main_dict.get(k, 0) + v
        main_dict[k] = newValue

def aoa2dict(aoa: list) -> dict:
    result = {}
    for el in aoa:
        result[el[0]] = el[1]
    return result

def split_ratings(ratings_aoa: list, test_size: float = 0.33) -> list:
    train = []
    test = []
    for r in ratings_aoa:
        test_length = round(len(r)*test_size)
        train_length = len(r) - test_length
        train_part = r[:train_length]
        test_part = r[train_length:]
        train.append(train_part)
        test.append(test_part)
    return train, test

def get_preference(ratings: list) -> list:
    full_preferences = list(filter(lambda x: x[1] > 0, ratings))
    full_preferences_sorted = list(sorted(full_preferences, key=lambda x: x[1], reverse=True))
    return lmap(lambda x: x[0], full_preferences_sorted)

def get_preferences(ratings_aoa: list) -> list:
    return lmap(get_preference, ratings_aoa)

def get_by_indexes(indexed_list, indexes):
    return lmap(lambda i: indexed_list[i], indexes)

def get_random_predictions(preferences, n_total_items, n_items, random_state):
    np.random.seed(random_state)
    result = []
    for preference in preferences:
        local_result = []
        pset = frozenset(preference)
        while len(local_result) < n_items:
            p = np.random.randint(n_total_items)
            if not pset.__contains__(p):
                local_result.append(p)
        result.append(local_result)
    return result


class U2UCF():
    def __init__(self, ratings_arrays) -> None:
        
        self.ratings = lmap(aoa2dict, ratings_arrays)
        
        self.item_users = {}

        for u in range(len(ratings_arrays)):
            for i in ratings_arrays[u]:
                iusers = self.item_users.get(i[0], set())
                iusers.add(u)
                self.item_users[i[0]] = iusers

        item_ratings = {}

        for r in self.ratings:
            add_ratings_dict(item_ratings, r)
        self.popular_list = sorted(item_ratings.keys(), key = lambda k: item_ratings[k], reverse=True)

    def recommend(self, user_index: int, n_items: int = 20, max_similar_users: int = 1000) -> list:
        user_ratings = self.ratings[user_index]
        rated_items = frozenset(user_ratings.keys())

        similar_users_set = set()

        for k in user_ratings.keys():
            similar_users_set.update(self.item_users.get(k, set()))

        if similar_users_set.__contains__(user_index):
            similar_users_set.remove(user_index)
        
        similar_users_with_corr = lmap(
            lambda u: [u, similarity(self.ratings[u], user_ratings)],
            similar_users_set
        )

        most_similar_users_with_corr = sorted(similar_users_with_corr, key=lambda x: x[1], reverse=True)[:max_similar_users]
        most_similar_users_with_corr = list(filter(
            lambda x: x[1] > 0,
            most_similar_users_with_corr
        ))

        items_ratings = {}

        for u in most_similar_users_with_corr:
            add_ratings_dict(items_ratings, self.ratings[u[0]])
        
        items2rec = sorted(items_ratings.keys(), key=lambda x: items_ratings[x], reverse=True)

        items2rec = list(filter(
            lambda x: not rated_items.__contains__(x),
            items2rec
        ))

        if len(items2rec) >= n_items:
            return items2rec[:n_items]
        
        nnset = frozenset(items2rec)

        for item in self.popular_list:
            if not nnset.__contains__(item):
                items2rec.append(item)
                if len(items2rec) >= n_items:
                    return items2rec
        
        return items2rec


 