import { getTreeData } from '../api/imageNetApi';
import { TreeNode } from '../types/TreeNode';

export const fetchTreeData = async (): Promise<TreeNode> => {
  try {
    return await getTreeData();
  } catch (error: any) {
    console.error('Error fetching tree data:', error);
    if (error.response) {
      console.error(error.response.data);
      console.error(error.response.status);
      console.error(error.response.headers);
    } else if (error.request) {
      // The request was made but no response was received
      console.error(error.request);
    } else {
      // Something happened in setting up the request that triggered an Error
      console.error('Error', error.message);
    }
    throw error;
  }
};