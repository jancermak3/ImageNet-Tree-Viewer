import axios from 'axios';
import { TreeNode } from '../types/TreeNode';

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL //'http://localhost:5248/api';

const imageNetApi = axios.create({
  baseURL: API_BASE_URL,
});

export const getTreeData = (): Promise<TreeNode> => {
  return imageNetApi.get<TreeNode>('/ImageNetItems/tree')
    .then(response => response.data);
};