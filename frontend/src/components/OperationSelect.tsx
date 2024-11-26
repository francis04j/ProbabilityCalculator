import React from 'react';
import { Operation } from '../api/probability';

interface OperationSelectProps {
  value: Operation;
  onChange: (value: Operation) => void;
}

export function OperationSelect({ value, onChange }: OperationSelectProps) {
  return (
    <div>
      <label htmlFor="operation" className="block text-sm font-medium text-gray-700 mb-1">
        Operation
      </label>
      <select
        id="operation"
        value={value}
        onChange={(e) => onChange(e.target.value as Operation)}
        className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-colors"
      >
        <option value="combinedWith">Combined With (P(A)P(B))</option>
        <option value="either">Either (P(A) + P(B) - P(A)P(B))</option>
      </select>
    </div>
  );
}